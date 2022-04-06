using Microsoft.AspNetCore.Mvc;
using MediatR;
using Microsoft.AspNetCore.Cors;

namespace TheExchangeApi.Areas.Admin.Products.GetAllProducts
{
    [Route("admin/products.all")]
    [ApiController]
    [EnableCors("myFrontendPolicy")]
    public class GetAllProductsController : ControllerBase
    {
        private readonly ISender _mediator;

        public GetAllProductsController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _mediator.Send(new GetAllProducts.GetAllProductsQuery());
            return Ok(products);
        }

    }
}
