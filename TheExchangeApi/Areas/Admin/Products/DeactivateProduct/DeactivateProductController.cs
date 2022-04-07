using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using TheExchangeApi.Models;

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
        public async Task<IActionResult> DeactivateProduct(string Id, bool IsActive)
        {
            var deactivatedProduct = _mediator.Send(new DeactivateProduct.DeactivateProductCommand(Id, IsActive));
            return Ok(deactivatedProduct);
        }
    }
}
