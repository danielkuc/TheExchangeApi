using MediatR;
using TheExchangeApi.Models;
using MongoDB.Driver;

namespace TheExchangeApi.Areas.Admin.Products.AddProduct
{
    public class AddProduct
    {
        public record Request : IRequest<Response>
        {
            public string Name { get; set; } = string.Empty;
            public string Description { get; set; } = string.Empty;
            public double Price { get; set; }
            public bool IsAvailable { get; set; }
            public int Quantity { get; set; }
            public string AddedBy { get; set; } = string.Empty;
        }

        public record Response;

        public class RequestHandler : IRequestHandler<Request, Response>
        {
            private readonly IMongoCollection<Product> _collection;

            public RequestHandler(IMongoCollection<Product> collection)
            {
                _collection = collection;
            }
            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var newProduct = new Product() 
                              { 
                                Name = request.Name,
                                Description = request.Description,
                                Price = request.Price,
                                IsAvailable = request.IsAvailable,
                                Quantity = request.Quantity,
                                AddedBy = request.AddedBy
                              };

                await _collection.InsertOneAsync(newProduct, cancellationToken: cancellationToken);

                return await Task.FromResult(new Response());
            }
        }
    }
}
