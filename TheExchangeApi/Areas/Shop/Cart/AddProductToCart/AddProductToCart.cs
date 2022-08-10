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
                var CartId = GetCartId();

                if (!_cartsCollection.AsQueryable().Any(c => c.CartId == CartId))
                {
                    var newShoppingCart = new ShoppingCart(CartId);
                    newShoppingCart.IncrementQuantity(productFromDb);
                    await _cartsCollection.InsertOneAsync(newShoppingCart, cancellationToken: cancellationToken);
                    return await Task.FromResult(new Response());
                }

                var shoppingCart = _cartsCollection.AsQueryable().Where(c => c.CartId == CartId).First();
                shoppingCart.IncrementQuantity(productFromDb);
                await _cartsCollection.ReplaceOneAsync(c => c.CartId == CartId, shoppingCart, cancellationToken: cancellationToken);
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
