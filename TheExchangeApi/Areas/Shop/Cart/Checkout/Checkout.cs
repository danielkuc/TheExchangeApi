using System;
using System.Linq;
using MediatR;
using TheExchangeApi.Models;

namespace TheExchangeApi.Areas.Shop.Cart.Checkout
{
    public class Checkout
    {
        public record Request(ShoppingCart Cart) : IRequest<Response>;
        public record Response(Guid OrderId);
        public class Handler : IRequestHandler<Request, Response>
        {
            public Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }
        }
    }
}
