using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TheExchangeApi.Controllers
{
    [ApiController]
    [Route("test")]
    public class TestController : ControllerBase
    {
        [HttpGet("one")]
        public string Get()
        {
            return "Hello from API";
        }
    }
}
