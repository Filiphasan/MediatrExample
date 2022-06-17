using MediatR;
using MediatrExample.CQRS.Auth.Login;
using MediatrExample.Shared.DataModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MediatrExample.API.Controllers
{
    public class AuthController : CustomBaseController
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(GenericResponse<LoginResponse>), 200)]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            return CreateResultInstance(await _mediator.Send(request));
        }
    }
}
