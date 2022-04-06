using MediatR;
using MongoDB.Bson;
using MongoDB.Driver;
using TheExchangeApi.Models;

namespace TheExchangeApi.Areas.Admin.Products.DeactivateProduct
{
    public class DeactivateProduct
    {
        public record DeactivateProductCommand(string Id, bool IsActive) : IRequest<Product>;

        public class DeactivateProductHandler : IRequestHandler<DeactivateProductCommand, Product>
        {
            private readonly IProductDatabaseSettings _settings;
            private readonly IMongoClient _client;

            public DeactivateProductHandler(IProductDatabaseSettings settings, IMongoClient client)
            {
                _settings = settings;
                _client = client;
            }
            public Task<Product> Handle(DeactivateProductCommand request, CancellationToken cancellationToken)
            {
                var database = _client.GetDatabase(_settings.DatabaseName);
                var collection = database.GetCollection<Product>(_settings.ProductsCollectionName).Find(new BsonDocument()).ToList();
                var originalProduct = collection.FirstOrDefault(dbProduct => dbProduct.Id == request.Id);

                if (originalProduct != null && originalProduct.IsAvailable == false)
                {
                    return Task.FromResult(originalProduct);
                }
                return Task.FromResult(originalProduct);
            }
        }
    }
}
