using MediatR;
using Microsoft.AspNetCore.Mvc;
using TheExchangeApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TheExchangeApi.Areas.Admin.Features.Products
{
    [Route("api/test/product")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ISender _mediator;

        public ProductsController(ISender mediator)
        {
            _mediator = mediator;
        }
        // GET: api/<ProductsController>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _mediator.Send(new GetAllProducts.GetAllProductsQuery());
            return Ok(products);
        }

        // GET api/<ProductsController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(string id)
        {
            var product = await _mediator.Send(new GetProductById.GetProductByIdQuery(id));
            return Ok(product);
        }

        // POST api/<ProductsController>
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

        // PUT api/<ProductsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ProductsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
