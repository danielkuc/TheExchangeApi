using MediatR;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using static TheExchangeApi.Areas.Admin.Products.ActivateProduct.ActivateProduct;

namespace TheExchangeApi.Areas.Admin.Products.ActivateProduct
{
    [Route("admin/product.activate")]
    public class ActivateProductController : AccessController
    {
        [HttpPost]
        public async Task<Response> Action(
            [FromBody] Request request,
            [FromServices] ISender mediator,
            CancellationToken cancellationToken) 
            => await mediator.Send(request, cancellationToken);
    }
}
