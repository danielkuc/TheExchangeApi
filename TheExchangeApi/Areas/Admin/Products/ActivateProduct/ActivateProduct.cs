using MediatR;
using MongoDB.Driver;
using TheExchangeApi.Models;

namespace TheExchangeApi.Areas.Admin.Products.ActivateProduct
{
    public class ActivateProduct
    {
        public record Request(string Id) : IRequest<Response>;
        public record Response();

        public class RequestHandler : IRequestHandler<Request, Response>
        {
            private readonly IMongoCollection<Product> _collection;

            public RequestHandler(IMongoCollection<Product> collection)
            {
                _collection = collection;
            }
            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var filterById = Builders<Product>.Filter.Eq(product => product.Id, request.Id);
                var activateProduct = Builders<Product>.Update.Set(p => p.IsAvailable, true);
                await _collection.UpdateOneAsync(filterById, activateProduct, cancellationToken: cancellationToken);
                return await Task.FromResult(new Response());
            }
        }
    }
}
