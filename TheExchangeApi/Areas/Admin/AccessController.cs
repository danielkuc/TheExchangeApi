using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace TheExchangeApi.Areas.Admin
{
    [ApiController]
    [EnableCors("theExchangeShopPolicy")]
    [Authorize(Policy = "WriteAccess")]
    public class AccessController : ControllerBase
    {

    }
}
