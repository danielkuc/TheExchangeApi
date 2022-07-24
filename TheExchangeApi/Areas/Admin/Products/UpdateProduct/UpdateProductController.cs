using MediatR;
using Microsoft.AspNetCore.Mvc;
using static TheExchangeApi.Areas.Admin.Products.UpdateProduct.UpdateProduct;

namespace TheExchangeApi.Areas.Admin.Products.UpdateProductFields
{
    [Route("admin/product.update")]
    public class UpdateProductController : AccessController
    {
        [HttpPost]
        public async Task<Response> Action(
            [FromBody] Request request,
            [FromServices]ISender mediator,
            CancellationToken cancellationToken
            ) => await mediator.Send(request, cancellationToken);
    }
}
