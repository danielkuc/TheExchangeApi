using Microsoft.AspNetCore.Mvc;
using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;

namespace TheExchangeApi.Areas.Admin.Products.GetAllProducts
{
    [ApiController]
    [Route("admin/products.all")]
    [EnableCors("myFrontendPolicy")]
    public class GetAllProductsController : ControllerBase
    {
        private readonly ISender _mediator;

        public GetAllProductsController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Authorize(Policy = "ReadAccess")]
        public async Task<IActionResult> GetAll()
        {
            var products = await _mediator.Send(new GetAllProducts.GetAllProductsQuery());
            return Ok(products);
        }

    }
}
