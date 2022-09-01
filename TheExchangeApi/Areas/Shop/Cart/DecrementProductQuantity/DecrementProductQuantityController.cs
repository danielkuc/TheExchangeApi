using MediatR;
using Microsoft.AspNetCore.Mvc;
using static TheExchangeApi.Areas.Shop.Cart.DecrementProductQuantity.DecrementProductQuantity;

namespace TheExchangeApi.Areas.Shop.Cart.DecrementProductQuantity
{
    [Route("shop/cart.product.decrement.quantity")]
    public class DecrementProductQuantityController : BaseShopController
    {
        [HttpPost]
        public async Task<Response> Action(
            [FromBody] Request request,
            [FromServices] ISender mediator,
            CancellationToken cancellationToken
            ) => await mediator.Send(request, cancellationToken);


    }
}
