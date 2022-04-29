using MediatR;
using MongoDB.Driver;
using MongoDB.Bson;
using TheExchangeApi.Models;

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
                var productCollection = _client.GetDatabase(_settings.DatabaseName).GetCollection<Product>(_settings.ProductsCollectionName);
                //var filters = Builders<Product>.Filter.Eq("name", request.Name);
                var products = productCollection.Find(product => product.Name.Contains(request.Name) && product.IsAvailable).ToList(cancellationToken: cancellationToken);

                return Task.FromResult(products);
            }
        }
    }
}
