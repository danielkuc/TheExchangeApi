using MediatR;
using TheExchangeApi.Models;
using MongoDB.Driver;
using MongoDB.Bson;

namespace TheExchangeApi.Areas.Admin.Features.Products
{
    public class GetAllProducts
    {
        public record GetAllProductsQuery : IRequest<List<Product>>;

        public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, List<Product>>
        {
            public Task<List<Product>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
            {
                var client = new MongoClient("mongodb+srv://admin:Testowanie1@theexchangedb.mqzo6.mongodb.net/productDatabase?retryWrites=true&w=majority");
                var db = client.GetDatabase("productDatabase");

                var products = db.GetCollection<Product>("products").Find(new BsonDocument()).ToList(cancellationToken: cancellationToken);

                return Task.FromResult(products);
            }
        }
    }
}
