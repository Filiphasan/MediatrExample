using MediatrExample.Shared.DataModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MediatrExample.API.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class CustomBaseController : ControllerBase
    {
        [NonAction]
        public IActionResult CreateResultInstance<T>(GenericResponse<T> response)
        {
            return new ObjectResult(response)
            {
                StatusCode = response.HttpCode
            };
        }
    }
}
