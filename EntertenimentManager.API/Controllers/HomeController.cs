﻿using EntertenimentManager.API.Data;
using Microsoft.AspNetCore.Mvc;

namespace EntertenimentManager.API.Controllers
{
    [ApiController]
    [Route("")]
    public class HomeController : ControllerBase
    {
        [HttpGet("")]
        public IActionResult Get()
        {
            return Ok("Env Dev");
        }
    }
}