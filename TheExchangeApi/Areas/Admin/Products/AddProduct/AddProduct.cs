using MediatR;
using TheExchangeApi.Models;
using MongoDB.Driver;

namespace TheExchangeApi.Areas.Admin.Products.AddProduct
{
    public class AddProduct
    {
        public record Request(Product PassedProduct) : IRequest<Response>;

        public record Response;

        public class RequestHandler : IRequestHandler<Request, Response>
        {
            private readonly IMongoCollection<Product> _collection;

            public RequestHandler(IMongoCollection<Product> collection)
            {
                _collection = collection;
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

                _collection.InsertOneAsync(newProduct, cancellationToken: cancellationToken);

                return Task.FromResult(new Response());
            }
        }
    }
}
