using Microsoft.AspNetCore.Mvc;

namespace MediatrExample.API.Controller
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class FirstController : ControllerBase
    {
        public FirstController()
        {

        }

        [HttpGet]
        public async Task<IActionResult> GetFirst()
        {
            return Ok("Server Working..");
        }
    }
}
