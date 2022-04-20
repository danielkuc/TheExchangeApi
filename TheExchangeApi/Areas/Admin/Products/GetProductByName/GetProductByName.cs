using MongoDB.Driver;
using MediatR;
using TheExchangeApi.Models;

namespace TheExchangeApi.Areas.Admin.Products.GetProductByName
{
    public class GetProductByName
    {
        public record GetProductByNameQuery(string productName) : IRequest<Product>;

        public class GetProductByNameHandler : IRequestHandler<GetProductByNameQuery, Product>
        {
            private readonly IProductDatabaseSettings _settings;
            private readonly IMongoClient _client;

            public GetProductByNameHandler(IProductDatabaseSettings settings, IMongoClient client)
            {
                _settings = settings;
                _client = client;
            }

            public Task<Product> Handle(GetProductByNameQuery request, CancellationToken cancellationToken)
            {
                var productDatabase = _client.GetDatabase(_settings.DatabaseName);

                var filterByName = Builders<Product>.Filter.Eq(product => product.Name, request.productName);

                var firstFoundProduct = productDatabase.GetCollection<Product>(_settings.ProductsCollectionName)
                    .Find(filterByName).FirstOrDefault(cancellationToken: cancellationToken);

                return Task.FromResult(firstFoundProduct);
            }
        }
    }
}
