using MediatR;
using Microsoft.AspNetCore.Mvc;
using static TheExchangeApi.Areas.Admin.Products.AddProduct.AddProduct;

namespace TheExchangeApi.Areas.Admin.Products.AddProduct
{
    [Route("admin/product.add")]
    public class AddProductController : AccessController
    {

        [HttpPost]
        public async Task<Response> Action(
            [FromBody] Request request,
            [FromServices] ISender mediator,
            CancellationToken cancellationToken)
            => await mediator.Send(request, cancellationToken);
    }
}
