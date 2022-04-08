using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;


namespace TheExchangeApi.Areas.Admin.Products.DeactivateProduct
{
    [Route("admin/product.deactivate")]
    [ApiController]
    [EnableCors("myFrontendPolicy")]
    public class DeactivateProductController : ControllerBase
    {
        private readonly ISender _mediator;

        public DeactivateProductController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpPut]
        public async Task<IActionResult> DeactivateProduct(string Id)
        {
            var deactivatedProduct = await _mediator.Send(new DeactivateProduct.DeactivateProductCommand(Id));
            return Ok(deactivatedProduct);
        }
    }
}
