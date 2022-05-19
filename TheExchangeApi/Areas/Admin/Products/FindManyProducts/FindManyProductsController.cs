using Microsoft.AspNetCore.Mvc;
using MediatR;
using static TheExchangeApi.Areas.Admin.Products.FindManyProducts.FindManyProducts;

namespace TheExchangeApi.Areas.Admin.Products.FindManyProducts
{
    [Route("admin/products.list")]
    public class FindManyProductsController : BaseAdminController
    {
        [HttpGet]
        public async Task<Response> Action(
            [FromQuery] Request request,
            [FromServices] ISender mediator,
            CancellationToken cancellationToken
            ) => await mediator.Send(request, cancellationToken);
    }
}
