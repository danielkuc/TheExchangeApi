using MongoDB.Driver;
using MongoDB.Bson;
using MediatR;
using TheExchangeApi.Models;

namespace TheExchangeApi.Areas.Admin.Products.GetProductById
{
    public class GetProductById
    {
        public record GetProductByIdQuery(string Id) : IRequest<Models.UpdateResult>;

        public class GetProductByIdHandler : IRequestHandler<GetProductByIdQuery, Models.UpdateResult>
        {
            private readonly IProductDatabaseSettings _settings;
            private readonly IMongoClient _client;

            public GetProductByIdHandler(IProductDatabaseSettings settings, IMongoClient client)
            {
                _settings = settings;
                _client = client;
            }

            public Task<Models.UpdateResult> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
            {
                var database = _client.GetDatabase(_settings.DatabaseName);

                var collection = database.GetCollection<UpdateResult>(_settings.ProductsCollectionName).Find(new BsonDocument()).ToList(cancellationToken: cancellationToken);

                var product = collection.FirstOrDefault(dbProduct => dbProduct.Id == request.Id);

                return Task.FromResult(product);

            }
        }

    }
}
