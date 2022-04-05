using MediatR;
using TheExchangeApi.Models;
using MongoDB.Driver;
using TheExchangeApi.Areas.Admin.Models;

namespace TheExchangeApi.Areas.Admin.Features.Products
{
    public class AddProduct
    {
        public class AddProductCommand : IRequest<Product>
        {
            public string Name { get; }
            public string Description { get; }
            public double Price { get; }

            public AddProductCommand(string name, string description, double price)
            {
                Name = name;
                Description = description;
                Price = price;
            }
        }

        public class AddProductHandler : IRequestHandler<AddProductCommand, Product>
        {
            private readonly IProductDatabaseSettings _settings;
            private readonly IMongoClient _client;

            public AddProductHandler(IProductDatabaseSettings settings, IMongoClient client)
            {
                _settings = settings;
                _client = client;
            }
            public Task<Product> Handle(AddProductCommand command, CancellationToken cancellationToken)
            {
                var db = _client.GetDatabase(_settings.DatabaseName);
                var products = db.GetCollection<Product>(_settings.ProductsCollectionName);

                var product = new Product() 
                              { 
                                Name = command.Name,
                                Description = command.Description,
                                Price = command.Price,
                              };

                products.InsertOne(product);
                return Task.FromResult(product);
            }
        }
    }
}
