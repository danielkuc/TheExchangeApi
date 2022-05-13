using MediatR;
using MongoDB.Driver;
using TheExchangeApi.Models;

namespace TheExchangeApi.Areas.Admin.Products.ActivateProduct
{
    public class ActivateProduct
    {
        public record Request(string Id) : IRequest<Response>;

        public record Response();

        public class ActivateProductHandler : IRequestHandler<Request, Response>
        {
            private readonly IProductDatabaseSettings _settings;
            private readonly IMongoClient _client;

            public ActivateProductHandler(IProductDatabaseSettings settings, IMongoClient client)
            {
                _settings = settings;
                _client = client;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var productDatabase = _client.GetDatabase(_settings.DatabaseName);
                var filterById = Builders<Product>.Filter.Eq(product => product.Id, request.Id);
                var activateProduct = Builders<Product>.Update.Set(p => p.IsAvailable, true);
                var updatedProduct = productDatabase.GetCollection<Product>(_settings.ProductsCollectionName)
                    .UpdateOneAsync(filterById, activateProduct, cancellationToken: cancellationToken);

                return await Task.FromResult(new Response());
            }
        }
    }
}
