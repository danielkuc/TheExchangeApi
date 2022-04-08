using MediatR;
using TheExchangeApi.Models;
using MongoDB.Driver;
using MongoDB.Bson;

namespace TheExchangeApi.Areas.Admin.Products.GetAllProducts
{
    public class GetAllProducts
    {
        public record GetAllProductsQuery : IRequest<List<Models.UpdateResult>>;

        public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, List<Models.UpdateResult>>
        {
            private readonly IProductDatabaseSettings _settings;
            private readonly IMongoClient _client;

            public GetAllProductsQueryHandler(IProductDatabaseSettings settings, IMongoClient client)
            {
                _settings = settings;
                _client = client;
            }

            public Task<List<Models.UpdateResult>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
            {
                var db = _client.GetDatabase(_settings.DatabaseName);

                var products = db.GetCollection<UpdateResult>(_settings.ProductsCollectionName).Find(new BsonDocument()).ToList(cancellationToken: cancellationToken);

                return Task.FromResult(products);
            }
        }
    }
}
