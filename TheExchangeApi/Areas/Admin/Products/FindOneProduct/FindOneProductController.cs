using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using static TheExchangeApi.Areas.Admin.Products.FindOneProduct.FindOneProduct;

namespace TheExchangeApi.Areas.Admin.Products.FindOneProduct
{
    [Route("admin/product.findOneProduct")]
    [ApiController]
    [EnableCors("theExchangeShopPolicy")]
    public class FindOneProductController : ControllerBase
    {
        [HttpGet]
        public async Task<Response> Action(
            [FromQuery] ProductRequest request,
            [FromServices] ISender mediator
            ) => await mediator.Send(request);
    }
}
