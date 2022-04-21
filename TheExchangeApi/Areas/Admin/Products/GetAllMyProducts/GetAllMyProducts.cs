using MediatR;
using MongoDB.Driver;
using MongoDB.Bson;
using TheExchangeApi.Models;

namespace TheExchangeApi.Areas.Admin.Products.GetAllMyProducts
{
    public class GetAllMyProducts
    {
        public record GetAllMyProductsQuery : IRequest<List<Product>>;

        public class GetAllMyProductsHandler : IRequestHandler<GetAllMyProductsQuery, List<Product>>
        {
            private readonly IProductDatabaseSettings _settings;
            private readonly IMongoClient _client;

            public GetAllMyProductsHandler(IProductDatabaseSettings settings, IMongoClient client)
            {
                _settings = settings;
                _client = client;
            }
            public Task<List<Product>> Handle(GetAllMyProductsQuery request, CancellationToken cancellationToken)
            {
                var db = _client.GetDatabase(_settings.DatabaseName);
                //var filterByUserName = Builders<Product>.Filter.Eq(product => product.AddedBy, request);

                var products = db.GetCollection<Product>(_settings.ProductsCollectionName)
                    .Find(new BsonDocument()).ToList(cancellationToken: cancellationToken);

                return Task.FromResult(products);
            }
        }
    }
}
