using MediatR;
using Microsoft.AspNetCore.Mvc;
using static TheExchangeApi.Areas.Admin.Products.UpdateProductPrice.UpdateProductPrice;

namespace TheExchangeApi.Areas.Admin.Products.UpdateProductPrice
{
    [Route("admin/product.update.price")]
    public class UpdateProductPriceController : AccessController
    {
        [HttpPost]
        public async Task<Response> Action(
            [FromBody] Request request,
            [FromServices] ISender mediator,
            CancellationToken cancellationToken
            ) => await mediator.Send(request, cancellationToken);


    }
}
