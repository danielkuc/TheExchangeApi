using MediatR;
using Microsoft.AspNetCore.Mvc;
using static TheExchangeApi.Areas.Shop.Cart.ViewMyCart.ViewMyCart;

namespace TheExchangeApi.Areas.Shop.Cart.ViewMyCart
{
    [Route("shop/cart.view")]
    public class ViewMyCartController : BaseShopController
    {
        [HttpGet]
        public async Task<Response> Action(
        [FromBody] Request request,
        [FromServices] ISender mediator,
        CancellationToken cancellationToken)
        => await mediator.Send(request, cancellationToken);
    }
}
