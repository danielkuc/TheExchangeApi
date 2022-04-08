using MediatR;
using MongoDB.Driver;
using TheExchangeApi.Models;

namespace TheExchangeApi.Areas.Admin.Products.ActivateProduct
{
    public class ActivateProduct
    {
        public record ActivateProductCommand(string Id) : IRequest<UpdateResult>;

        public class ActivateProductHandler : IRequestHandler<ActivateProductCommand, UpdateResult>
        {
            private readonly IProductDatabaseSettings _settings;
            private readonly IMongoClient _client;

            public ActivateProductHandler(IProductDatabaseSettings settings, IMongoClient client)
            {
                _settings = settings;
                _client = client;
            }

            public Task<UpdateResult> Handle(ActivateProductCommand request, CancellationToken cancellationToken)
            {
                var productDatabase = _client.GetDatabase(_settings.DatabaseName);
                var filterById = Builders<Product>.Filter.Eq(product => product.Id, request.Id);
                var activateProduct = Builders<Product>.Update.Set(p => p.IsAvailable, true);
                var updateProduct = productDatabase.GetCollection<Product>(_settings.ProductsCollectionName)
                    .UpdateOneAsync(filterById, activateProduct);

                return updateProduct;
            }
        }
    }
}
