using MediatR;
using Microsoft.AspNetCore.Mvc;
using static TheExchangeApi.Areas.Admin.Products.DeactivateProduct.DeactivateProduct;

namespace TheExchangeApi.Areas.Admin.Products.DeactivateProduct
{
    [Route("admin/product.deactivate")]
    public class DeactivateProductController : AccessController
    {
        [HttpPost]
        public async Task<Response> Action(
            [FromBody] Request request,
            [FromServices] ISender mediator,
            CancellationToken cancellationToken)
            => await mediator.Send(request, cancellationToken);
    }
}
