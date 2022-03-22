using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TheExchangeApi.Areas.Shop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShopController : ControllerBase
    {
        [EnableCors("myFrontendPolicy")]
        [HttpGet]
        public string Get()
        {
            return "Hello from the Shop Controller";
        }

        //get product
        //get products
    }
}
