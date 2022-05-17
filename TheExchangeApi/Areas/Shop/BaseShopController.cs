using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace TheExchangeApi.Areas.Shop
{
    [ApiController]
    [EnableCors("theExchangeShopPolicy")]
    public class BaseShopController : ControllerBase
    {
    }
}
