using MediatR;
using TheExchangeApi.Models;
using MongoDB.Driver;

namespace TheExchangeApi.Areas.Admin.Products.AddProduct
{
    public class AddProduct
    {
        public record Request(Product PassedProduct) : IRequest<Response>;

        public record Response;

        public class AddProductHandler : IRequestHandler<Request, Response>
        {
            private readonly IProductDatabaseSettings _settings;
            private readonly IMongoClient _client;

            public AddProductHandler(IProductDatabaseSettings settings, IMongoClient client)
            {
                _settings = settings;
                _client = client;
            }
            public Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var newProduct = new Product() 
                              { 
                                Name = request.PassedProduct.Name,
                                Description = request.PassedProduct.Description,
                                Price = request.PassedProduct.Price,
                                IsAvailable = request.PassedProduct.IsAvailable,
                                Quantity = request.PassedProduct.Quantity,
                                AddedBy = request.PassedProduct.AddedBy
                              };

                var productDatabase = _client.GetDatabase(_settings.DatabaseName);

                productDatabase.GetCollection<Product>(_settings.ProductsCollectionName)
                    .InsertOneAsync(newProduct, cancellationToken: cancellationToken);

                return Task.FromResult(new Response());
            }
        }
    }
}
