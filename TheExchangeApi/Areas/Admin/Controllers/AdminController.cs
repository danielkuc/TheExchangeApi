using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TheExchangeApi.Areas.Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        [EnableCors("myFrontendPolicy")]
        [HttpGet]
        public string Get()
        {
            return "Hello from the Admin Controller";
        }


        //crud Product
    }
}
