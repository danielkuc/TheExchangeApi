using MediatR;
using TheExchangeApi.Models;
using MongoDB.Driver;

namespace TheExchangeApi.Areas.Admin.Features.Products
{
    public class GetAllProducts
    {
        public record GetAllProductsQuery : IRequest<IMongoCollection<Product>>;

        public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, IMongoCollection<Product>>
        {
            public Task<IMongoCollection<Product>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
            {
                var client = new MongoClient("mongodb+srv://admin:Testowanie1@theexchangedb.mqzo6.mongodb.net/productDatabase?retryWrites=true&w=majority");
                var db = client.GetDatabase("productDatabase");
                var products = db.GetCollection<Product>("products");
                return Task.FromResult(products);
            }
        }
    }
}
