using MediatR;
using MongoDB.Driver;
using TheExchangeApi.Models;
using FluentValidation;

namespace TheExchangeApi.Areas.Admin.Products.DeactivateProduct
{
    public class DeactivateProduct
    {
        public record Request(Product Product) : IRequest<Response>;
        public record Response();

        public class RequestHandler : IRequestHandler<Request, Response>
        {
            private readonly IMongoCollection<Product> _collection;

            public RequestHandler(IMongoCollection<Product> collection)
            {
                _collection = collection;
            }
            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                await _collection.FindOneAndUpdateAsync(
                    p => p.Id == request.Product.Id,
                    Builders<Product>.Update.Set(p => p.IsActive, false),
                    cancellationToken: cancellationToken
                    );
                return await Task.FromResult(new Response());
            }
        }
    }

    public class DeactivateProductValidator : AbstractValidator<DeactivateProduct.Request>
    {
        public DeactivateProductValidator()
        {
            RuleFor(p => p.Product.IsActive).NotEmpty();
        }
    }
}
