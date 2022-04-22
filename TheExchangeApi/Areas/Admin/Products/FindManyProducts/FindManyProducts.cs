using MediatR;
using MongoDB.Driver;
using MongoDB.Bson;
using TheExchangeApi.Models;

namespace TheExchangeApi.Areas.Admin.Products.FindManyProducts
{
    public class FindManyProducts
    {
        public record FindManyProductsQuery : IRequest<List<Product>>;

        public class FindManyProductsHandler : IRequestHandler<FindManyProductsQuery, List<Product>>
        {
            private readonly IProductDatabaseSettings _settings;
            private readonly IMongoClient _client;

            public FindManyProductsHandler(IProductDatabaseSettings settings, IMongoClient client)
            {
                _settings = settings;
                _client = client;
            }

            public Task<List<Product>> Handle(FindManyProductsQuery request, CancellationToken cancellationToken)
            {
                var db = _client.GetDatabase(_settings.DatabaseName);

                var products = db.GetCollection<Product>(_settings.ProductsCollectionName)
                    .Find(new BsonDocument()).ToList(cancellationToken: cancellationToken);

                return Task.FromResult(products);
            }
        }
    }
}
