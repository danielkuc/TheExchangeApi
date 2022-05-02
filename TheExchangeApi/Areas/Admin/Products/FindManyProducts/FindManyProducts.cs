using MediatR;
using MongoDB.Driver;
using MongoDB.Bson;
using TheExchangeApi.Models;
using System.Text.RegularExpressions;

namespace TheExchangeApi.Areas.Admin.Products.FindManyProducts
{
    public class FindManyProducts
    {
        public record FindManyProductsQuery(string Name) : IRequest<List<Product>>;

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
                var productDatabase = _client.GetDatabase(_settings.DatabaseName);
                var productCollection = productDatabase.GetCollection<Product>(_settings.ProductsCollectionName);
                var filter = Builders<Product>.Filter.Regex("name", new BsonRegularExpression(new Regex("Test")));

                var products = productCollection.Find(filter).ToList();


                return Task.FromResult(products);
            }
        }
    }
}
