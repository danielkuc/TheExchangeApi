using MediatR;
using Microsoft.AspNetCore.Mvc;
using static TheExchangeApi.Areas.Shop.Products.FindOneProduct.FindOneProduct;

namespace TheExchangeApi.Areas.Shop.Products.FindOneProduct
{
    [Route("shop/product.findOneProduct")]
    public class FindOneProductController : BaseShopController
    {
        [HttpGet]
        public async Task<Response> Action(
            [FromQuery] ProductRequest request,
            [FromServices] ISender mediator,
            CancellationToken cancellationToken
            ) => await mediator.Send(request, cancellationToken);
    }
}
