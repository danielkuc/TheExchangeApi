using MediatR;
using TheExchangeApi.Models;
using MongoDB.Driver;

namespace TheExchangeApi.Areas.Shop.Cart.AddProductToCart
{
    public class AddProductToCart
    {
        public record Request(Product Product) : IRequest<Response>;

        public record Response;
        public class RequestHandler : IRequestHandler<Request, Response>
        {
            private readonly IMongoCollection<Product> _productCollection;
            private readonly IMongoCollection<ShoppingCart> _cartsCollection;
            private readonly IHttpContextAccessor _httpContextAccessor;

            public RequestHandler(
                IMongoCollection<Product> productCollection
                , IMongoCollection<ShoppingCart> cartsCollection
                , IHttpContextAccessor accessor
                )
            {
                _productCollection = productCollection;
                _cartsCollection = cartsCollection;
                _httpContextAccessor = accessor;
            }
            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var productFromDb = _productCollection.AsQueryable().Where(x => x.Id == request.Product.Id).Single();
                // var newCartId = GetCartId();
                var newCartId = Guid.NewGuid().ToByteArray();
                var newShoppingCart = new ShoppingCart(newCartId);
                newShoppingCart.IncrementQuantity(productFromDb);
                await _cartsCollection.InsertOneAsync(newShoppingCart, cancellationToken: cancellationToken);
                return await Task.FromResult(new Response());
            }

            private byte[] GetCartId()
            {
                var cartId = new byte[] { };

                if (!_httpContextAccessor.HttpContext.Session.TryGetValue("CartId", out cartId))
                {
                    var newId = Guid.NewGuid().ToByteArray();
                    _httpContextAccessor.HttpContext.Session.Set("CartId", newId);
                    cartId = newId;
                }
                return cartId;
            }
        }
    }
}
