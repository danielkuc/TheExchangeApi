using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace TheExchangeApi.Areas.Admin.Products.GetProductById
{
    [Route("admin/product.get/{id}")]
    [ApiController]
    [EnableCors("myFrontendPolicy")]
    public class GetProductByIdController : ControllerBase
    {
        private readonly ISender _mediator;

        public GetProductByIdController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetProductById(string Id)
        {
            var product = await _mediator.Send(new GetProductById.GetProductByIdQuery(Id));
            return Ok(product);
        }
    }
}
