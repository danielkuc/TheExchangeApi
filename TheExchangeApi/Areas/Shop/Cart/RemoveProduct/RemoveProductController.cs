using MediatR;
using Microsoft.AspNetCore.Mvc;
using static TheExchangeApi.Areas.Shop.Cart.RemoveProduct.RemoveProduct;

namespace TheExchangeApi.Areas.Shop.Cart.RemoveProduct
{
    [Route("shop/cart.product.remove")]
    public class RemoveProductController : BaseShopController
    {
        [HttpPost]
        public async Task<Response> Action(
            [FromBody] Request request,
            [FromServices] ISender mediator,
            CancellationToken cancellationToken
            ) => await mediator.Send(request, cancellationToken);


    }
}