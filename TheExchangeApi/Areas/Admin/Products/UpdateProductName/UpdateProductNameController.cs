using MediatR;
using Microsoft.AspNetCore.Mvc;
using static TheExchangeApi.Areas.Admin.Products.UpdateProductName.UpdateProductName;

namespace TheExchangeApi.Areas.Admin.Products.UpdateProductName
{
    [Route("admin/product.update.name")]
    public class UpdateProductNameController : AccessController
    {
        [HttpPost]
        public async Task<Response> Action(
            [FromBody] Request request,
            [FromServices] ISender mediator,
            CancellationToken cancellationToken
            ) => await mediator.Send(request, cancellationToken);


    }
}
