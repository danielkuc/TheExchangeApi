using MediatR;
using MongoDB.Driver;
using MongoDB.Bson;
using TheExchangeApi.Models;

namespace TheExchangeApi.Areas.Shop.Products.FindManyProducts
{
    public class FindManyProducts
    {
        public record FindManyProductsQuery(string name) : IRequest<List<Product>>;

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
                var productCollection = _client.GetDatabase(_settings.DatabaseName).GetCollection<Product>(_settings.ProductsCollectionName);
                var filters = Builders<Product>.Filter.Eq("name", request.name);
                var products = productCollection.Find(filters).ToList(cancellationToken: cancellationToken);

                return Task.FromResult(products);
            }
        }
    }
}
