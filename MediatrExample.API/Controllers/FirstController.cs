using MediatrExample.Shared.CustomMethod;
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
        [ProducesResponseType(typeof(string), 200)]
        public async Task<IActionResult> GetFirst()
        {
            var date = DateTime.Now;

            return Ok(date.GetDateTimeToMyString());
        }
    }
}
