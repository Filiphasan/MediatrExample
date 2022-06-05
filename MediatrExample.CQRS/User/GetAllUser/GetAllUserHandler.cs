using MediatR;
using MediatrExample.Core.Interfaces.Data;
using MediatrExample.Shared.CustomExceptions;
using MediatrExample.Shared.DataModels;
using MediatrExample.Shared.DataModels.User.GetAllUser;
using Microsoft.AspNetCore.Http;

namespace MediatrExample.CQRS.User.GetAllUser
{
    public class GetAllUserHandler : BaseHandler<GetAllUserRequest, GenericResponse<GetAllUserResponse>>, IRequestHandler<GetAllUserRequest, GenericResponse<GetAllUserResponse>>
    {
        private readonly IUserRepository _userRepository;

        public GetAllUserHandler(IHttpContextAccessor httpContextAccessor, IEnumerable<FluentValidation.IValidator<GetAllUserRequest>> validators, IUserRepository userRepository) : base(httpContextAccessor, validators)
        {
            _userRepository = userRepository;
        }

        public async Task<GenericResponse<GetAllUserResponse>> Handle(GetAllUserRequest request, CancellationToken cancellationToken)
        {
            var validErr = await CheckValidate(request);
            if (validErr.Any()) throw new ValidationException(validErr);
            var response = new GetAllUserResponse();
            try
            {
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
                return GenericResponse<GetAllUserResponse>.Error(500, ex.Message);
            }
        }
    }
}
