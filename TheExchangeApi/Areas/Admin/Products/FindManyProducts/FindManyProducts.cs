using MediatR;
using MongoDB.Driver;
using MongoDB.Bson;
using TheExchangeApi.Models;
using System.Text.RegularExpressions;

namespace TheExchangeApi.Areas.Admin.Products.FindManyProducts
{
    public class FindManyProducts
    {
        public record FindManyProductsQuery(string Name, string PriceFrom, string PriceTo) : IRequest<List<Product>>;

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
                var filter = Builders<Product>.Filter.Eq(product => product.IsAvailable, true);

                if (!string.IsNullOrWhiteSpace(request.Name))
                {
                    var nameFilter = Builders<Product>.Filter.
                        Regex("name", new BsonRegularExpression(new Regex(request.Name, RegexOptions.IgnoreCase)));
                    filter &= nameFilter;
                }

                if (!string.IsNullOrWhiteSpace(request.PriceFrom) && !string.IsNullOrWhiteSpace(request.PriceTo))
                {
                    var convertedPriceFrom = Convert.ToDouble(request.PriceFrom);
                    var convertedPriceTo = Convert.ToDouble(request.PriceTo);
                    var priceFilter = Builders<Product>.Filter.Gt("price",convertedPriceFrom) & Builders<Product>.Filter.Lt("price", convertedPriceTo);
                    filter &= priceFilter;
                }

                var products = productCollection.Find(filter).ToList(cancellationToken: cancellationToken);

                return Task.FromResult(products);
            }
        }
    }
}
