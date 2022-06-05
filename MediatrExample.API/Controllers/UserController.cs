using MediatR;
using MediatrExample.CQRS.User.GetAllUser;
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

        [HttpPost("list")]
        public async Task<IActionResult> GetUserList([FromBody]GetAllUserRequest request)
        {
            return CreateResultInstance(await _mediator.Send(request));
        }
    }
}
