using MediatR;
using Microsoft.AspNetCore.Mvc;
using static TheExchangeApi.Areas.Admin.Products.UpdateProductDescription.UpdateProductDescription;

namespace TheExchangeApi.Areas.Admin.Products.UpdateProductDescription
{
    [Route("admin/product.update.description")]
    public class UpdateProductDescriptionController : AccessController
    {
        [HttpPost]
        public async Task<Response> Action(
            [FromBody] Request request,
            [FromServices] ISender mediator,
            CancellationToken cancellationToken
            ) => await mediator.Send(request, cancellationToken);


    }
}
