using MediatR;
using MongoDB.Driver;
using TheExchangeApi.Models;

namespace TheExchangeApi.Areas.Admin.Products.FindManyProducts
{
    public class FindManyProducts
    {
        public record Request(string? Name, double? PriceFrom, double? PriceTo) : IRequest<Response>;
        public record Response(IReadOnlyList<Product> ProductList);

        public class RequestHandler : IRequestHandler<Request, Response>
        {
            private readonly IMongoCollection<Product> _collection;

            public RequestHandler(IMongoCollection<Product> collection) 
                => _collection = collection;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                //extention method intead of using Where a lot
                var query = _collection.AsQueryable().Where(p => p.IsActive);

                if (!string.IsNullOrWhiteSpace(request.Name))
                    query = query.Where(p => p.Name.Contains(request.Name));

                if (request.PriceFrom.HasValue)
                    query = query.Where(p => p.Price >= request.PriceFrom);

                if (request.PriceTo.HasValue)
                    query = query.Where(p => p.Price <= request.PriceTo);

                return await Task.FromResult(new Response(query.ToList().AsReadOnly()));
            }
        }
    }
}
