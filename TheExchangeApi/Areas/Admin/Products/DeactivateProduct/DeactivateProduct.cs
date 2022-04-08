using MediatR;
using MongoDB.Bson;
using MongoDB.Driver;
using TheExchangeApi.Models;

namespace TheExchangeApi.Areas.Admin.Products.DeactivateProduct
{
    public class DeactivateProduct
    {
        public record DeactivateProductCommand(string Id) : IRequest<MongoDB.Driver.UpdateResult>;

        public class DeactivateProductHandler : IRequestHandler<DeactivateProductCommand, MongoDB.Driver.UpdateResult>
        {
            private readonly IProductDatabaseSettings _settings;
            private readonly IMongoClient _client;

            public DeactivateProductHandler(IProductDatabaseSettings settings, IMongoClient client)
            {
                _settings = settings;
                _client = client;
            }
            public Task<MongoDB.Driver.UpdateResult> Handle(DeactivateProductCommand request, CancellationToken cancellationToken)
            {
                var database = _client.GetDatabase(_settings.DatabaseName);
                var filter = Builders<BsonDocument>.Filter.Eq("Id", request.Id);
                var update = Builders<BsonDocument>.Update.Set("IsAvailable", false);
                var updated = database.GetCollection<BsonDocument>(_settings.ProductsCollectionName)
                    .UpdateOne(filter, update);

                return Task.FromResult(updated);
            }
        }
    }
}
