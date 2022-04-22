using MongoDB.Driver;
using MediatR;
using TheExchangeApi.Models;

namespace TheExchangeApi.Areas.Admin.Products.FindOneProduct
{
    public class FindOneProduct
    {
        public record FindOneProductQuery(string Id) : IRequest<Product>;

        public class FindOneProductHandler : IRequestHandler<FindOneProductQuery, Product>
        {
            private readonly IProductDatabaseSettings _settings;
            private readonly IMongoClient _client;

            public FindOneProductHandler(IProductDatabaseSettings settings, IMongoClient client)
            {
                _settings = settings;
                _client = client;
            }

            public Task<Product> Handle(FindOneProductQuery request, CancellationToken cancellationToken)
            {
                var productDatabase = _client.GetDatabase(_settings.DatabaseName);

                var filterById = Builders<Product>.Filter.Eq(product => product.Id, request.Id);

                var firstFoundProduct = productDatabase.GetCollection<Product>(_settings.ProductsCollectionName)
                    .Find(filterById).FirstOrDefault(cancellationToken: cancellationToken);

                return Task.FromResult(firstFoundProduct);
            }
        }

    }
}
