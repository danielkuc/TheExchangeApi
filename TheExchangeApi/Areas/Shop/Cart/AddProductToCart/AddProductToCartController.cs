using MediatR;
using Microsoft.AspNetCore.Mvc;
using static TheExchangeApi.Areas.Shop.Cart.AddProductToCart.AddProductToCart;

namespace TheExchangeApi.Areas.Shop.Cart.AddProductToCart
{
    [Route("shop/cart.add")]
    public class AddProductToCartController : BaseShopController
    {
        [HttpPost]
        public async Task<Response> Action(
            [FromBody] Request request,
            [FromServices] ISender mediator,
            CancellationToken cancellationToken)
            => await mediator.Send(request, cancellationToken);

    }
}
