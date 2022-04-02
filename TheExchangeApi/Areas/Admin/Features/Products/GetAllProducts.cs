using MediatR;
using TheExchangeApi.Models;
using MongoDB.Driver;

namespace TheExchangeApi.Areas.Admin.Features.Products
{
    public class GetAllProducts
    {
        //input
        public class Query : IRequest<RetreivedProducts>
        {
        }

        //output
        public class RetreivedProducts
        {
            public MongoCollectionBase<Product> ProductList;
        }

        //handler
        public class Handler : IRequestHandler<Query, RetreivedProducts>
        {
            public Task<RetreivedProducts> Handle(Query request, CancellationToken cancellationToken)
            {
                var mongoClient = new MongoClient("mongodb+srv://admin:Testowanie1@theexchangedb.mqzo6.mongodb.net/productDatabase?retryWrites=true&w=majority");
                var database = mongoClient.GetDatabase("productDatabase");
                var products = database.GetCollection<Product>("products");
                return (Task<RetreivedProducts>)products;
            }
        }
    }
}
