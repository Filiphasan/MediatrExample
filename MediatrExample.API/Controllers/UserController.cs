using MediatR;
using MediatrExample.CQRS.User.AddUser;
using MediatrExample.CQRS.User.GetAllUser;
using MediatrExample.Shared.DataModels;
using Microsoft.AspNetCore.Mvc;

namespace MediatrExample.API.Controllers
{
    public class UserController : CustomBaseController
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("list")]
        [ProducesResponseType(typeof(GenericResponse<GetAllUserResponse>),200)]
        public async Task<IActionResult> GetUserList([FromBody]GetAllUserRequest request)
        {
            return CreateResultInstance(await _mediator.Send(request));
        }

        [HttpPost("add")]
        [ProducesResponseType(typeof(GenericResponse<AddUserResponse>), 200)]
        public async Task<IActionResult> AddUser([FromBody]AddUserRequest request)
        {
            return CreateResultInstance(await _mediator.Send(request));
        }
    }
}
