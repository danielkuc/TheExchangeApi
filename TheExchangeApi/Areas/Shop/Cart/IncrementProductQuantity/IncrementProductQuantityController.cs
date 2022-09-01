using MediatR;
using Microsoft.AspNetCore.Mvc;
using static TheExchangeApi.Areas.Shop.Cart.IncrementProductQuantity.IncrementProductQuantity;

namespace TheExchangeApi.Areas.Shop.Cart.IncrementProductQuantity
{
    [Route("shop/cart.product.increment.quantity")]
    public class IncrementProductQuantityController : BaseShopController
    {
        [HttpPost]
        public async Task<Response> Action(
            [FromBody] Request request,
            [FromServices] ISender mediator,
            CancellationToken cancellationToken
            ) => await mediator.Send(request, cancellationToken);


    }
}
