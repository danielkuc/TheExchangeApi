using MediatR;
using MongoDB.Driver;
using TheExchangeApi.Models;

namespace TheExchangeApi.Areas.Admin.Products.DeactivateProduct
{
    public class DeactivateProduct
    {
        public record Request(string Id) : IRequest<Response>;
        public record Response;

        public class RequestHandler : IRequestHandler<Request, Response>
        {
            private readonly IMongoCollection<Product> _collection;

            public RequestHandler(IMongoCollection<Product> collection)
            {
                _collection = collection;
            }
            public Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var filterById = Builders<Product>.Filter.Eq(product => product.Id, request.Id);
                var deactivateProduct = Builders<Product>.Update.Set(p => p.IsAvailable, false);
                var updatedProduct = _collection.
                    UpdateOneAsync(
                    filterById,
                    deactivateProduct,
                    cancellationToken: cancellationToken
                    );

                return Task.FromResult(new Response());
            }
        }
    }
}
