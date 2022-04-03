using MediatR;
using MongoDB.Driver;
using TheExchangeApi.Areas.Admin.Models;
using TheExchangeApi.Models;

namespace TheExchangeApi.Areas.Admin.Features.Products
{
    public class GetProductById
    {
        //Query/Command
        //All the data needed to execute
        public record ProductByIdQuery(int Id) : IRequest<RetreivedProduct>;

        public class Handler : IRequestHandler<ProductByIdQuery, RetreivedProduct>
        {
            private IMongoCollection<Product> _products;

            public Handler(IProductDatabaseSettings settings, IMongoClient mongoClient)
            {
                var database = mongoClient.GetDatabase(settings.DatabaseName);
                _products = database.GetCollection<Product>(settings.ProductCollectionName);
            }
            public Task<RetreivedProduct> Handle(ProductByIdQuery request, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }
        }

        public record RetreivedProduct(int Id, string Name, string description, double price);
    }
}
