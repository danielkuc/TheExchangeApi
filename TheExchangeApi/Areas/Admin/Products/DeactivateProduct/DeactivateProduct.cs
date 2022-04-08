using MediatR;
using MongoDB.Bson;
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
                var database = _client.GetDatabase(_settings.DatabaseName);
                var filter = Builders<Product>.Filter.Eq(product => product.Id, request.Id);
                var update = Builders<Product>.Update.Set(p => p.IsAvailable, false);
                var updated = database.GetCollection<Product>(_settings.ProductsCollectionName)
                    .UpdateOneAsync(filter, update);

                return updated;
            }
        }
    }
}
