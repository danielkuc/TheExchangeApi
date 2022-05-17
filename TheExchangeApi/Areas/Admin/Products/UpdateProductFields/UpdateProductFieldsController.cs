using MediatR;
using Microsoft.AspNetCore.Mvc;
using static TheExchangeApi.Areas.Admin.Products.UpdateProductFields.UpdateProductFields;

namespace TheExchangeApi.Areas.Admin.Products.UpdateProductFields
{
    [Route("admin/product.update")]
    public class UpdateProductFieldsController : AccessController
    {
        [HttpPost]
        public async Task<Response> Action(
            [FromBody] Request request,
            [FromServices]ISender mediator,
            CancellationToken cancellationToken
            ) => await mediator.Send(request, cancellationToken);
    }
}
