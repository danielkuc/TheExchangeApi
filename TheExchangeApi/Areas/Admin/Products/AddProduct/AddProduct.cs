using MediatR;
using TheExchangeApi.Models;
using MongoDB.Driver;

namespace TheExchangeApi.Areas.Admin.Products.AddProduct
{
    public class AddProduct
    {
        public record AddProductCommand(Product PassedProduct) : IRequest<Product>;

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
                var newProduct = new Product() 
                              { 
                                Name = command.PassedProduct.Name,
                                Description = command.PassedProduct.Description,
                                Price = command.PassedProduct.Price,
                                IsAvailable = command.PassedProduct.IsAvailable,
                                Quantity = command.PassedProduct.Quantity,
                                AddedBy = command.PassedProduct.AddedBy
                              };

                var productDatabase = _client.GetDatabase(_settings.DatabaseName);

                productDatabase.GetCollection<Product>(_settings.ProductsCollectionName)
                    .InsertOneAsync(newProduct, cancellationToken: cancellationToken);

                return Task.FromResult(newProduct);
            }
        }
    }
}
