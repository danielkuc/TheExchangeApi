using MediatR;
using MongoDB.Driver;
using TheExchangeApi.Models;

namespace TheExchangeApi.Areas.Shop.Products.FindManyProducts
{
    public class FindManyProducts
    {
        public record Request(string? Name, double? PriceFrom, double? PriceTo) : IRequest<Response>;
        public record Response(List<Product> ProductList);

        public class RequestHandler : IRequestHandler<Request, Response>
        {
            private readonly IMongoCollection<Product> _collection;

            public RequestHandler(IMongoCollection<Product> collection)
            {
                _collection = collection;
            }

            public Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var query = _collection.AsQueryable().Where(p => p.IsAvailable);

                if (!string.IsNullOrWhiteSpace(request.Name))
                    query = query.Where(p => p.Name.Contains(request.Name));

                if (request.PriceFrom.HasValue)
                    query = query.Where(p => p.Price >= request.PriceFrom);

                if (request.PriceTo.HasValue)
                    query = query.Where(p => p.Price <= request.PriceTo);
                var result = new Response(query.ToList());

                return Task.FromResult(new Response(query.ToList()));
            }
        }
    }
}
