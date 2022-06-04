using Microsoft.AspNetCore.Mvc;

namespace MediatrExample.API.Controllers
{
    [Route("/")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            return Ok("Server Work");
        }
    }
}
