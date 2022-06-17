using FluentValidation;
using MediatR;
using MediatrExample.Core.Interfaces.Data;
using MediatrExample.Core.Interfaces.Service;
using MediatrExample.Shared.CustomExceptions;
using MediatrExample.Shared.DataModels;
using Microsoft.AspNetCore.Http;
using EnLock;

namespace MediatrExample.CQRS.User.ForgotPwUser
{
    public class ForgotPwUserHandler : BaseHandler<ForgotPwUserRequest, GenericResponse<ForgotPwUserResponse>, ForgotPwUserHandler>, IRequestHandler<ForgotPwUserRequest, GenericResponse<ForgotPwUserResponse>>
    {
        private readonly IUserRepository _userRepository;
        public ForgotPwUserHandler(IHttpContextAccessor httpContextAccessor, IEnumerable<IValidator<ForgotPwUserRequest>> validators, ILogHelper<ForgotPwUserHandler> logHelper, IUserRepository userRepository) : base(httpContextAccessor, validators, logHelper)
        {
            _userRepository = userRepository;
        }

        public async Task<GenericResponse<ForgotPwUserResponse>> Handle(ForgotPwUserRequest request, CancellationToken cancellationToken)
        {
            await CheckValidate(request);
            try
            {
                var response = new ForgotPwUserResponse();

                var user = await _userRepository.Where(x => x.Mail == request.Mail).ToFirstOrDefaultWithNoLockAsync();
                if (user is null)
                    throw new MyHttpException("Email not found!");

                // TO DO : Send email to user with password reset link
                //
                //TO DO : Send email to user with password reset link
                
                return GenericResponse<ForgotPwUserResponse>.Success(200, "Password reset link has been sent to your e-mail address.", response);
            }
            catch (Exception ex)
            {
                _logHelper.LogError(ex);
                return GenericResponse<ForgotPwUserResponse>.Error(500, ex.Message);
            }
        }
    }
}
