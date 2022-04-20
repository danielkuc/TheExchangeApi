using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace TheExchangeApi.Areas.Admin.Products.GetProductByName
{
    [Route("admin/product.get.byName")]
    [ApiController]
    [EnableCors("myFrontendPolicy")]
    public class GetProductByNameController : ControllerBase
    {
        private readonly ISender _mediator;

        public GetProductByNameController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetProductByName(string Name)
        {
            var product = await _mediator.Send(new GetProductByName.GetProductByNameQuery(Name));
            return Ok(product);
        }
    }
}
