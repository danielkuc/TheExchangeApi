using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TheExchangeApi.Controllers
{
    [Route("test")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [EnableCors("myFrontendPolicy")]
        [HttpGet]
        public string Get()
        {
            return "Hello from the API";
        }
    }
}
