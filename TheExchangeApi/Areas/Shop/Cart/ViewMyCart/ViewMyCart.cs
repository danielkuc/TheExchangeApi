using MediatR;
using TheExchangeApi.Models;
using MongoDB.Driver;

namespace TheExchangeApi.Areas.Shop.Cart.ViewMyCart;

public class ViewMyCart
{
    public record Request : IRequest<Response>;
    public record Response(ShoppingCart ShoppingCartFromDB);
    public class RequestHandler : IRequestHandler<Request, Response>
    {
        private readonly IMongoCollection<ShoppingCart> _collection;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RequestHandler(IMongoCollection<ShoppingCart> collection, IHttpContextAccessor accessor)
        {
            _collection = collection;
            _httpContextAccessor = accessor;
        }

        public Task<Response> Handle(Request request, CancellationToken cancellationToken)
        {
            if (_httpContextAccessor.HttpContext.Session.GetString("CartId") == null)
            {
               throw new ArgumentNullException("Cart doestn't exist");
            }

            var cartId = _httpContextAccessor.HttpContext.Session.GetString("CartId");
            var shoppingCartFromDB = _collection.AsQueryable().Where(c => c.Id == cartId).First();
            return Task.FromResult(new Response(shoppingCartFromDB));
        }
    }
}
