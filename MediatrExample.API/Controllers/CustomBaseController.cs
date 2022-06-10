using MediatrExample.Shared.DataModels;
using Microsoft.AspNetCore.Mvc;

namespace MediatrExample.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomBaseController<TController> : ControllerBase
    {
        protected readonly ILogger<TController> _logger;

        public CustomBaseController(ILogger<TController> logger)
        {
            _logger = logger;
        }

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
