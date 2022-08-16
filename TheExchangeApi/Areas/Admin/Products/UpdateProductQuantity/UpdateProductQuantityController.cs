using MediatR;
using Microsoft.AspNetCore.Mvc;
using static TheExchangeApi.Areas.Admin.Products.UpdateProductQuantity.UpdateProductQuantity;

namespace TheExchangeApi.Areas.Admin.Products.UpdateProductQuantity
{
    [Route("admin/product.update.quantity")]
    public class UpdateProductQuantityController : AccessController
    {
        [HttpPost]
        public async Task<Response> Action(
            [FromBody] Request request,
            [FromServices] ISender mediator,
            CancellationToken cancellationToken
            ) => await mediator.Send(request, cancellationToken);


    }
}
