using FluentValidation;
using MediatR;
using MediatrExample.Core.Interfaces.Data;
using MediatrExample.Core.Interfaces.Service;
using MediatrExample.Shared.CustomMethod;
using MediatrExample.Shared.DataModels;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace MediatrExample.CQRS.User.AddUser
{
    public class AddUserHandler : BaseHandler<AddUserRequest, GenericResponse<AddUserResponse>, AddUserHandler>, IRequestHandler<AddUserRequest, GenericResponse<AddUserResponse>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IHashService _hashService;
        public AddUserHandler(IHttpContextAccessor httpContextAccessor, IEnumerable<IValidator<AddUserRequest>> validators, ILogger<AddUserHandler> logger, IUserRepository userRepository, IHashService hashService) : base(httpContextAccessor, validators, logger)
        {
            _userRepository = userRepository;
            _hashService = hashService;
        }

        public async Task<GenericResponse<AddUserResponse>> Handle(AddUserRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var response = new AddUserResponse();
                string pwHash = await _hashService.SetSHA256HashAsync(request.Password);
                await _hashService.DisposeAsync();

                var userEntity = new Core.Entities.User()
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Mail = request.Email,
                    Gsm = request.Gsm,
                    PasswordHash = pwHash,
                };

                var user = await _userRepository.InsertAsyncReturn(userEntity);

                response.FirstName = user.FirstName;
                response.LastName = user.LastName;
                response.Id = user.Id;
                response.Gsm = user.Gsm;
                response.Mail = user.Mail;
                
                return GenericResponse<AddUserResponse>.Success(200, response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ExpectExceptionMessage());
                return GenericResponse<AddUserResponse>.Error(500, ex.Message);
            }
        }
    }
}
