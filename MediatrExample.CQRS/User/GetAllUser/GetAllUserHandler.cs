using MediatR;
using MediatrExample.Core.Interfaces.Data;
using MediatrExample.Shared.CustomExceptions;
using MediatrExample.Shared.CustomMethod;
using MediatrExample.Shared.DataModels;
using MediatrExample.Shared.DataModels.User.GetAllUser;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace MediatrExample.CQRS.User.GetAllUser
{
    public class GetAllUserHandler : BaseHandler<GetAllUserRequest, GenericResponse<GetAllUserResponse>, GetAllUserHandler>, IRequestHandler<GetAllUserRequest, GenericResponse<GetAllUserResponse>>
    {
        private readonly IUserRepository _userRepository;

        public GetAllUserHandler(IHttpContextAccessor httpContextAccessor, IEnumerable<FluentValidation.IValidator<GetAllUserRequest>> validators, ILogger<GetAllUserHandler> logger, IUserRepository userRepository) : base(httpContextAccessor, validators, logger)
        {
            _userRepository = userRepository;
        }

        public async Task<GenericResponse<GetAllUserResponse>> Handle(GetAllUserRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var response = new GetAllUserResponse();
                var userList = await _userRepository.GetAllAsync();
                var data = userList.Select(x => new UserDataModel
                {
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Gsm = x.Gsm,
                    Id = x.Id,
                    Mail = x.Mail,
                });
                response.TotalCount = userList.Count();
                response.UserList = data;
                return GenericResponse<GetAllUserResponse>.Success(200, response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ExpectExceptionMessage());
                return GenericResponse<GetAllUserResponse>.Error(500, ex.Message);
            }
        }
    }
}
