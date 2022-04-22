using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MediatR;

namespace TheExchangeApi.Areas.Admin.Products.GetAllMyProducts
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("theExchangeShopPolicy")]
    public class GetAllMyProductsController : ControllerBase
    {
        private readonly ISender _mediator;

        public GetAllMyProductsController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]

        public async Task<IActionResult> Get(string userEmail)
        {
            var products = await _mediator.Send(new GetAllMyProducts.GetAllMyProductsQuery(userEmail));

            return Ok(products);
        }
    }
}
