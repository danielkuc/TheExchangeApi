using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace TheExchangeApi.Areas.Admin.Products.ActivateProduct
{
    [Route("admin/product.activate")]
    [ApiController]
    [EnableCors("theExchangeShopPolicy")]
    public class ActivateProductController : ControllerBase
    {
        private readonly ISender _mediator;

        public ActivateProductController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpPut]
        [Authorize(Policy = "WriteAccess")]
        public async Task<IActionResult> ActivateProduct(string Id)
        {
            var ActivatedProduct = await _mediator.Send(new ActivateProduct.ActivateProductCommand(Id));
            return Ok(ActivatedProduct);
        }
    }
}
