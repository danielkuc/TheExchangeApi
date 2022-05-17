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
                var filterById = Builders<Product>.Filter.Eq(product => product.Id, request.Id);

                var firstFoundProduct = _collection
                    .Find(filterById)
                    .FirstOrDefault(cancellationToken: cancellationToken);

                return Task.FromResult(new Response(firstFoundProduct));
            }
        }

    }
}
