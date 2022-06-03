using MediatR;
using TheExchangeApi.Models;
using MongoDB.Driver;

namespace TheExchangeApi.Areas.Shop.Cart.AddProductToCart
{
    public class AddProductToCart
    {
        public record Request(CartItem CartItem) : IRequest<Response>;

        public record Response;
        public class RequestHandler : IRequestHandler<Request, Response>
        {
            private readonly IMongoCollection<Product> _collection;
            private readonly IHttpContextAccessor _accessor;

            public RequestHandler(IMongoCollection<Product> collection, IHttpContextAccessor accessor)
            {
                _collection = collection;
                _accessor = accessor;
            }
            public Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                if (_accessor.HttpContext.Request.Cookies["cart_id"] == null)
                {

                }
                throw new NotImplementedException();
            }
        }
    }
}
