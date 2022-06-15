using FluentValidation;
using MediatR;
using MediatrExample.Core.Interfaces.Data;
using MediatrExample.Core.Interfaces.Service;
using MediatrExample.Shared.CustomExceptions;
using MediatrExample.Shared.CustomMethod;
using MediatrExample.Shared.DataModels;
using Microsoft.AspNetCore.Http;

namespace MediatrExample.CQRS.User.UpdatePwUser
{
    public class UpdatePwUserHandler : BaseHandler<UpdatePwUserRequest, GenericResponse<UpdatePwUserResponse>, UpdatePwUserHandler>, IRequestHandler<UpdatePwUserRequest, GenericResponse<UpdatePwUserResponse>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IHashService _hashService;
        public UpdatePwUserHandler(IHttpContextAccessor httpContextAccessor, IEnumerable<IValidator<UpdatePwUserRequest>> validators, ILogHelper<UpdatePwUserHandler> logHelper, IUserRepository userRepository, IHashService hashService) : base(httpContextAccessor, validators, logHelper)
        {
            _userRepository = userRepository;
            _hashService = hashService;
        }

        public async Task<GenericResponse<UpdatePwUserResponse>> Handle(UpdatePwUserRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var response = new UpdatePwUserResponse();

                var user = await _userRepository.GetByIdAsync(request.UserId);

                var isOldPWCorrect = await _hashService.CheckSHA256HashAsync(user.PasswordHash, request.OldPassword);
                
                if (!isOldPWCorrect)
                    throw new MyHttpException("Old Password is Incorrect!");

                user.PasswordHash = await _hashService.SetSHA256HashAsync(request.NewPassword);
                _userRepository.Update(user);

                response = user.ObjectMapper<UpdatePwUserResponse, Core.Entities.User>();

                return GenericResponse<UpdatePwUserResponse>.Success(response);
            }
            catch (Exception ex)
            {
                _logHelper.LogError(ex);
                return GenericResponse<UpdatePwUserResponse>.Error(500, ex.Message);
            }
        }
    }
}
