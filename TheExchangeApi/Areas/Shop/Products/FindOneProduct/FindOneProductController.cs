using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace TheExchangeApi.Areas.Shop.Products.FindOneProduct
{
    [Route("admin/product.findOneProduct")]
    [ApiController]
    [EnableCors("theExchangeShopPolicy")]
    public class FindOneProductController : ControllerBase
    {
        private readonly ISender _mediator;

        public FindOneProductController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> FindOneProductById(string Id)
        {
            var product = await _mediator.Send(new FindOneProduct.FindOneProductQuery(Id));
            return Ok(product);
        }
    }
}
