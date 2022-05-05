using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using TheExchangeApi.Models;

namespace TheExchangeApi.Areas.Admin.Products.UpdateProductFields
{
    [ApiController]
    [Route("admin/product.update")]
    [EnableCors("theExchangeShopPolicy")]
    public class UpdateProductFieldsController : ControllerBase
    {
        private readonly ISender _mediator;

        public UpdateProductFieldsController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpPut]
        [Authorize(Policy = "WriteAccess")]
        public async Task<IActionResult> Update(Product productUpdate)
        {
            var updatedProduct = await _mediator.Send(new UpdateProductFields.UpdateProductFieldsCommand(productUpdate));

            return Ok(updatedProduct);
        }
    }
}
