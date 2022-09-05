using MediatR;
using TheExchangeApi.Models;
using MongoDB.Driver;
using FluentValidation;

namespace TheExchangeApi.Areas.Admin.Products.AddProduct
{
    public class AddProduct
    {
        public record Request : IRequest<Response>
        {
            public string Name { get; set; } = string.Empty;
            public string Description { get; set; } = string.Empty;
            public double Price { get; set; }
            public bool IsActive { get; set; }
            public int Quantity { get; set; }
            public IFormFile Image { get; set; }
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
                                IsActive = request.IsActive,
                                Quantity = request.Quantity
                              };

                await _collection.InsertOneAsync(newProduct, cancellationToken: cancellationToken);

                return await Task.FromResult(new Response());
            }
        }
    }

    public class AddProductValidator : AbstractValidator<AddProduct.Request>
    {
        public AddProductValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty()
                .Length(3, 20)
                .WithMessage("Product name should be between 3 and 20 characters");
            RuleFor(p => p.Description)
                .NotEmpty()
                .Length(5, 25)
                .WithMessage("Product description should be between 5 and 25 characters");
            RuleFor(p => p.Price)
                .NotEmpty()
                .GreaterThanOrEqualTo(0);
            RuleFor(p => p.IsActive)
                .NotEmpty();
            RuleFor(p => p.Quantity)
                .NotEmpty()
                .GreaterThanOrEqualTo(0);
        }
    }
}
