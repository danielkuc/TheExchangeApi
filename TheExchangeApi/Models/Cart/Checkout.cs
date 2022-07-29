using System;
using MediatR;

namespace TheExchangeApi.Models.Cart
{
    public class Checkout
    {
        public class Request : IRequest<Response>
        {
            public ShoppingCart Cart {get; set;}
        }

        public class Response
        {
            public Guid OrderId { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            public Handler()
            {

            }
            public Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }
        }
    }
}
