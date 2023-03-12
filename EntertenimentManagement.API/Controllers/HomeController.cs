using Microsoft.AspNetCore.Mvc;

namespace EntertenimentManagement.API.Controllers
{
    [ApiController]
    [Route("")]
    public class HomeController : ControllerBase
    {
        [HttpGet("")]
        public IActionResult Get()
        {
            return Ok("Hello World");
        }        
    }
}
