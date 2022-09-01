using MediatR;
using TheExchangeApi.Models;
using MongoDB.Driver;

namespace TheExchangeApi.Areas.Shop.Cart.IncrementProductQuantity
{
    public class IncrementProductQuantity
    {
        public record Request(Product product) : IRequest<Response>;
        public record Response;
        public class RequestHandler : IRequestHandler<Request, Response>
        {
            private readonly IMongoCollection<Product> _productCollection;
            private readonly IMongoCollection<ShoppingCart> _cartCollection;
            private readonly IHttpContextAccessor _httpContextAccessor;

            public RequestHandler(
                  IMongoCollection<Product> productCollection
                , IMongoCollection<ShoppingCart> cartCollection
                , IHttpContextAccessor accessor
                )
            {
                _productCollection = productCollection;
                _cartCollection = cartCollection;
                _httpContextAccessor = accessor;
            }
            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var productFromDb = _productCollection.AsQueryable().Where(x => x.Id == request.product.Id).Single();
                if (_httpContextAccessor.HttpContext.Session.GetString("CartId") == null)
                {
                    throw new ArgumentNullException("Cart doesn't exist");
                }

                var cartId = _httpContextAccessor.HttpContext.Session.GetString("CartId");
                var cartFromDB = _cartCollection.AsQueryable().Where(c => c.Id == cartId).Single();
                cartFromDB.IncrementQuantity(productFromDb);
                await _cartCollection
                    .ReplaceOneAsync(c => c.Id == cartId, cartFromDB, cancellationToken: cancellationToken);
                return await Task.FromResult(new Response());
            }
        }
    }
}
