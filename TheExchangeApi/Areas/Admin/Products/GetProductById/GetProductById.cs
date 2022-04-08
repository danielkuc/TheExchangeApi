using MongoDB.Driver;
using MongoDB.Bson;
using MediatR;
using TheExchangeApi.Models;

namespace TheExchangeApi.Areas.Admin.Products.GetProductById
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
                var database = _client.GetDatabase(_settings.DatabaseName);
                var filterById = Builders<Product>.Filter.Eq(product => product.Id, request.Id);
                var firstFoundProduct = database.GetCollection<Product>(_settings.ProductsCollectionName)
                    .Find(filterById).FirstOrDefault(cancellationToken: cancellationToken);

                return Task.FromResult(firstFoundProduct);

            }
        }

    }
}
