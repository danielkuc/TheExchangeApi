using MongoDB.Driver;
using MediatR;
using TheExchangeApi.Models;

namespace TheExchangeApi.Areas.Shop.Products.FindOneProduct
{
    public class FindOneProduct
    {
        public record ProductRequest(string Id) : IRequest<Response>;
        public record Response(Product Product);

        public class RequestHandler : IRequestHandler<ProductRequest, Response>
        {
            private readonly IMongoCollection<Product> _collection;

            public RequestHandler(IMongoCollection<Product> collection)
            {
                _collection = collection;
            }

            public Task<Response> Handle(ProductRequest request, CancellationToken cancellationToken)
            {
                var firstFoundProduct = _collection.AsQueryable()
                    .First(x => x.Id == request.Id);

                return Task.FromResult(new Response(firstFoundProduct));
            }
        }

    }
}
