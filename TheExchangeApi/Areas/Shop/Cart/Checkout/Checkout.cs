using MediatR;
using TheExchangeApi.Models;
using MongoDB.Driver;
namespace TheExchangeApi.Areas.Shop.Cart.Checkout
{
    public class Checkout
    {
        public record Request(Customer Customer) : IRequest<Response>;
        public record Response(Guid OrderId);
        public class RequestHandler : IRequestHandler<Request, Response>
        {
            private readonly IMongoCollection<ShoppingCart> _cartColletion;
            private readonly IHttpContextAccessor _httpContextAccessor;

            public RequestHandler(IMongoCollection<ShoppingCart> cartColletion, IHttpContextAccessor accessor)
            {
                _cartColletion = cartColletion;
                _httpContextAccessor = accessor;
            }
            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                if (_httpContextAccessor.HttpContext.Session.GetString("CartId") == null)
                {
                    throw new ArgumentNullException("Cart is null");
                }

                var cartId = _httpContextAccessor.HttpContext.Session.GetString("CartId");
                var cartFromDb = _cartColletion.AsQueryable().Where(c => c.Id == cartId).Single();
                var newOrder = new Order(request.Customer, cartFromDb);
                return await Task.FromResult(new Response(newOrder.Id));
            }
        }
    }
}
