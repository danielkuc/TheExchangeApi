using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TheExchangeApi.Areas.Services;
using TheExchangeApi.Models;

namespace TheExchangeApi.Areas.Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly ProductService _productService;

        public AdminController(ProductService productService)
        {
            _productService = productService;
        }

        [EnableCors("myFrontendPolicy")]
        [HttpGet]
        public string Get()
        {
            return "Hello from the Admin Controller";
        }


        //crud Product
        [EnableCors("myFrontendPolicy")]
        [HttpPost]
        public ActionResult<Product> Create(Product product)
        {
            _productService.Create(product);
            return CreatedAtRoute("GetProduct", product);
        }
    }
}
