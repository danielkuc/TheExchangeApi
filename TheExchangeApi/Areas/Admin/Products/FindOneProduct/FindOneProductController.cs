using MediatR;
using Microsoft.AspNetCore.Mvc;
using static TheExchangeApi.Areas.Admin.Products.FindOneProduct.FindOneProduct;

namespace TheExchangeApi.Areas.Admin.Products.FindOneProduct
{
    [Route("admin/product.findOneProduct")]
    public class FindOneProductController : BaseAdminController
    {
        [HttpGet]
        public async Task<Response> Action(
            [FromQuery] ProductRequest request,
            [FromServices] ISender mediator,
            CancellationToken cancellationToken
            ) => await mediator.Send(request, cancellationToken);
    }
}
