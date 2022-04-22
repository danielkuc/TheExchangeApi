using Microsoft.AspNetCore.Mvc;
using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;

namespace TheExchangeApi.Areas.Admin.Products.FindManyProducts
{
    [ApiController]
    [Route("admin/product.findManyProducts")]
    [EnableCors("theExchangeShopPolicy")]
    public class FindManyProductsController : ControllerBase
    {
        private readonly ISender _mediator;

        public FindManyProductsController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> FindManyProducts()
        {
            var products = await _mediator.Send(new FindManyProducts.FindManyProductsQuery());
            return Ok(products);
        }

    }
}
