using MediatR;
using TheExchangeApi.Models;
using MongoDB.Driver;

namespace TheExchangeApi.Areas.Admin.Products.AddProduct
{
    public class AddProduct
    {
        public class AddProductCommand : IRequest<Models.UpdateResult>
        {
            public string Name { get; }
            public string Description { get; }
            public double Price { get; }
            public bool IsAvailable { get; }
            public int Quantity { get; }

            public AddProductCommand(string name, string description, double price, bool isAvailable, int quantity)
            {
                Name = name;
                Description = description;
                Price = price;
                IsAvailable = isAvailable;
                Quantity = quantity;
            }
        }

        public class AddProductHandler : IRequestHandler<AddProductCommand, Models.UpdateResult>
        {
            private readonly IProductDatabaseSettings _settings;
            private readonly IMongoClient _client;

            public AddProductHandler(IProductDatabaseSettings settings, IMongoClient client)
            {
                _settings = settings;
                _client = client;
            }
            public Task<Models.UpdateResult> Handle(AddProductCommand command, CancellationToken cancellationToken)
            {
                var db = _client.GetDatabase(_settings.DatabaseName);
                var products = db.GetCollection<Models.UpdateResult>(_settings.ProductsCollectionName);

                var product = new Models.UpdateResult() 
                              { 
                                Name = command.Name,
                                Description = command.Description,
                                Price = command.Price,
                                IsAvailable = command.IsAvailable,
                                Quantity = command.Quantity
                              };

                products.InsertOne(product);
                return Task.FromResult(product);
            }
        }
    }
}
