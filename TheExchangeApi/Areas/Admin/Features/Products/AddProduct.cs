using MediatR;
using TheExchangeApi.Models;
using MongoDB.Driver;

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
            public Task<Product> Handle(AddProductCommand command, CancellationToken cancellationToken)
            {
                var client = new MongoClient("mongodb+srv://admin:Testowanie1@theexchangedb.mqzo6.mongodb.net/productDatabase?retryWrites=true&w=majority");
                var db = client.GetDatabase("productDatabase");
                var products = db.GetCollection<Product>("products");

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
