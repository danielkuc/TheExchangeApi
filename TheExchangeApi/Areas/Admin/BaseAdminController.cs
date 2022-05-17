﻿using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace TheExchangeApi.Areas.Admin
{
    [ApiController]
    [EnableCors("theExchangeShopPolicy")]
    public class BaseAdminController : ControllerBase
    {
    }
}
