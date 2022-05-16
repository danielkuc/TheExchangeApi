using Microsoft.AspNetCore.Mvc;
using MediatR;
using static TheExchangeApi.Areas.Admin.Products.FindManyProducts.FindManyProducts;

namespace TheExchangeApi.Areas.Admin.Products.FindManyProducts
{
    [Route("admin/product.findManyProducts")]
    public class FindManyProductsController : AccessController
    {
        [HttpGet]
        public async Task<Result> Action(
            [FromQuery] Request request,
            [FromServices] ISender mediator
            ) => await mediator.Send(request);
    }
}
