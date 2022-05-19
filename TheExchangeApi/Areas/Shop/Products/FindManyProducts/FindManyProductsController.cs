using Microsoft.AspNetCore.Mvc;
using MediatR;
using static TheExchangeApi.Areas.Shop.Products.FindManyProducts.FindManyProducts;

namespace TheExchangeApi.Areas.Shop.Products.FindManyProducts
{
    [Route("shop/products.list")]
    public class FindManyProductsController : BaseShopController
    {
        [HttpGet]
        public async Task<Response> Action(
            [FromQuery] Request request,
            [FromServices] ISender mediator,
            CancellationToken cancellationToken
            ) => await mediator.Send(request, cancellationToken);
    }
}
