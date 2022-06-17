using FluentValidation;
using MediatR;
using MediatrExample.Core.Interfaces.Data;
using MediatrExample.Core.Interfaces.Service;
using MediatrExample.Shared.CustomExceptions;
using MediatrExample.Shared.CustomMethod;
using MediatrExample.Shared.DataModels;
using Microsoft.AspNetCore.Http;
using EnLock;

namespace MediatrExample.CQRS.User.GetUser
{
    public class GetUserHandler : BaseHandler<GetUserRequest, GenericResponse<GetUserResponse>, GetUserHandler>, IRequestHandler<GetUserRequest, GenericResponse<GetUserResponse>>
    {
        private readonly IUserRepository _userRepository;
        public GetUserHandler(IHttpContextAccessor httpContextAccessor, IEnumerable<IValidator<GetUserRequest>> validators, ILogHelper<GetUserHandler> logHelper, IUserRepository userRepository) : base(httpContextAccessor, validators, logHelper)
        {
            _userRepository = userRepository;
        }

        public async Task<GenericResponse<GetUserResponse>> Handle(GetUserRequest request, CancellationToken cancellationToken)
        {
            await CheckValidate(request);
            try
            {
                var response = new GetUserResponse();
                var user = await _userRepository.Where(x => x.Id == request.UserId).ToFirstOrDefaultWithNoLockAsync();
                if (user is null)
                    throw new MyHttpException("User not found!");

                response = user.ObjectMapper<GetUserResponse, Core.Entities.User>();

                return GenericResponse<GetUserResponse>.Success(response);
            }
            catch (Exception ex)
            {
                _logHelper.LogError(ex);
                throw new MyHttpException(ex.Message);
            }
        }
    }
}
