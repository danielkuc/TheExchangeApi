using Microsoft.AspNetCore.Mvc;
using TheExchangeApi.Areas.Admin.Services;
using TheExchangeApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TheExchangeApi.Areas.Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService productService;

        public ProductsController(IProductService productService)
        {
            this.productService = productService;
        }

        // GET: api/<ProductsController>
        [HttpGet]
        public ActionResult<List<Product>> Get() => productService.Get();

        // GET api/<ProductsController>/5
        [HttpGet("{id}")]
        public ActionResult<Product> Get(string id)
        {
            var product = productService.Get(id);

            if (product == null)
            {
                return NotFound($"Product not found");
            }
            return product;
        }

        // POST api/<ProductsController>
        [HttpPost]
        public ActionResult<Product> Post([FromBody] Product product)
        {
            productService.Create(product);
            return CreatedAtAction(nameof(Get), new{ id = product.Id }, product);
        }

        // PUT api/<ProductsController>/5
        [HttpPut("{id}")]
        public ActionResult Put(string id, [FromBody] Product product)
        {
            var existingProduct = productService.Get(id);
            if (existingProduct == null)
            {
                return NotFound("Product not found");
            }

            productService.Update(id, product);

            return NoContent();

        }

        // DELETE api/<ProductsController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            var product = productService.Get(id);

            if (product == null)
            {
                return NotFound("Product not found");
            }

            productService.Remove(id);

            return Ok("Product removed");

        }
    }
}
