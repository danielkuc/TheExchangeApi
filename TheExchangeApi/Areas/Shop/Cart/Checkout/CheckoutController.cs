using MediatR;
using Microsoft.AspNetCore.Mvc;
using static TheExchangeApi.Areas.Shop.Cart.Checkout.Checkout;

namespace TheExchangeApi.Areas.Shop.Cart.Checkout
{
    [Route("shop/cart.checkout")]
    public class CheckoutController : BaseShopController
    {
        [HttpPost]
        public async Task<Response> Action(
            [FromBody] Request request,
            [FromServices] ISender mediator,
            CancellationToken cancellationToken
            ) => await mediator.Send(request, cancellationToken);


    }
}
