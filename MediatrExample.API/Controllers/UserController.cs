using MediatR;
using MediatrExample.CQRS.User.AddUser;
using MediatrExample.CQRS.User.GetAllUser;
using MediatrExample.Shared.DataModels;
using Microsoft.AspNetCore.Mvc;

namespace MediatrExample.API.Controllers
{
    public class UserController : CustomBaseController<UserController>
    {
        private readonly IMediator _mediator;

        public UserController(ILogger<UserController> logger, IMediator mediator) : base(logger)
        {
            _mediator = mediator;
        }

        [HttpPost("list")]
        [ProducesResponseType(typeof(GenericResponse<GetAllUserResponse>),200)]
        public async Task<IActionResult> GetUserList([FromBody]GetAllUserRequest request)
        {
            return CreateResultInstance(await _mediator.Send(request));
        }

        [HttpPost]
        [ProducesResponseType(typeof(GenericResponse<AddUserResponse>), 200)]
        public async Task<IActionResult> AddUser([FromBody]AddUserRequest request)
        {
            return CreateResultInstance(await _mediator.Send(request));
        }
    }
}
