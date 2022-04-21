using MediatR;
using MongoDB.Driver;
using MongoDB.Bson;
using TheExchangeApi.Models;

namespace TheExchangeApi.Areas.Admin.Products.GetAllMyProducts
{
    public class GetAllMyProducts
    {
        public record GetAllMyProductsQuery(string UserEmail) : IRequest<List<Product>>;

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
                var productDatabase = _client.GetDatabase(_settings.DatabaseName);
                var filterByUserName = Builders<Product>.Filter.Eq(product => product.AddedBy, request.UserEmail);

                var products = productDatabase.GetCollection<Product>(_settings.ProductsCollectionName)
                    .Find(filterByUserName).ToList(cancellationToken: cancellationToken);

                return Task.FromResult(products);
            }
        }
    }
}
