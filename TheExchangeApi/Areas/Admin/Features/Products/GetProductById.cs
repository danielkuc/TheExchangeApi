using MongoDB.Driver;
using MongoDB.Bson;
using MediatR;
using TheExchangeApi.Models;
using TheExchangeApi.Areas.Admin.Models;

namespace TheExchangeApi.Areas.Admin.Features.Products
{
    public class GetProductById
    {
        public record GetProductByIdQuery(string Id) : IRequest<Product>;

        public class GetProductByIdHandler : IRequestHandler<GetProductByIdQuery, Product>
        {
            private readonly IProductDatabaseSettings _settings;
            private readonly IMongoClient _client;

            public GetProductByIdHandler(IProductDatabaseSettings settings, IMongoClient client)
            {
                _settings = settings;
                _client = client;
            }

            public Task<Product> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
            {
                var db = _client.GetDatabase(_settings.DatabaseName);

                var products = db.GetCollection<Product>(_settings.ProductsCollectionName).Find(new BsonDocument()).ToList(cancellationToken: cancellationToken);

                var product = products.FirstOrDefault(dbProduct => dbProduct.Id == request.Id);

                return Task.FromResult(product);

            }
        }

    }
}
