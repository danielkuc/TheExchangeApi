using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using TheExchangeApi.Models;

namespace TheExchangeApi.Areas.Admin.Products.AddProduct
{
    [Route("admin/product.add")]
    [ApiController]
    [EnableCors("myFrontendPolicy")]
    public class AddProductController : ControllerBase
    {
        private readonly ISender _mediator;

        public AddProductController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewProduct(Product newProduct)
        {
            var product = await _mediator.Send(new AddProduct.AddProductCommand(
                newProduct.Name,
                newProduct.Description,
                newProduct.Price,
                newProduct.IsAvailable,
                newProduct.Quantity
                ));

            return Ok(product);
        }
    }
}
