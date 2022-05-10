using MediatR;
using MongoDB.Driver;
using TheExchangeApi.Models;

namespace TheExchangeApi.Areas.Admin.Products.DeactivateProduct
{
    public class DeactivateProduct
    {
        public record DeactivateProductCommand(string Id) : IRequest<UpdateResult>;

        public class DeactivateProductHandler : IRequestHandler<DeactivateProductCommand, UpdateResult>
        {
            private readonly IProductDatabaseSettings _settings;
            private readonly IMongoClient _client;

            public DeactivateProductHandler(IProductDatabaseSettings settings, IMongoClient client)
            {
                _settings = settings;
                _client = client;
            }
            public Task<UpdateResult> Handle(DeactivateProductCommand request, CancellationToken cancellationToken)
            {
                var productDatabase = _client.GetDatabase(_settings.DatabaseName);
                var filterById = Builders<Product>.Filter.Eq(product => product.Id, request.Id);
                var deactivateProduct = Builders<Product>.Update.Set(p => p.IsAvailable, false);
                var updatedProduct = productDatabase.GetCollection<Product>(_settings.ProductsCollectionName)
                    .UpdateOneAsync(filterById, deactivateProduct, cancellationToken: cancellationToken);

                return updatedProduct;
            }
        }
    }
}
