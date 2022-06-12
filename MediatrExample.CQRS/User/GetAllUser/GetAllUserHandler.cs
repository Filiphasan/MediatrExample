using MediatR;
using MediatrExample.Core.Interfaces.Data;
using MediatrExample.Core.Interfaces.Service;
using MediatrExample.Shared.CustomMethod;
using MediatrExample.Shared.DataModels;
using MediatrExample.Shared.DataModels.User.GetAllUser;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace MediatrExample.CQRS.User.GetAllUser
{
    public class GetAllUserHandler : BaseHandler<GetAllUserRequest, GenericResponse<GetAllUserResponse>, GetAllUserHandler>, IRequestHandler<GetAllUserRequest, GenericResponse<GetAllUserResponse>>
    {
        private readonly IUserRepository _userRepository;

        public GetAllUserHandler(IHttpContextAccessor httpContextAccessor, IEnumerable<FluentValidation.IValidator<GetAllUserRequest>> validators, ILogHelper<GetAllUserHandler> logHelper, IUserRepository userRepository) : base(httpContextAccessor, validators, logHelper)
        {
            _userRepository = userRepository;
        }

        public async Task<GenericResponse<GetAllUserResponse>> Handle(GetAllUserRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var response = new GetAllUserResponse();
                var query = _userRepository.GetUserList(request.Query);

                var data = await query.Select(x => new UserDataModel
                {
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Gsm = x.Gsm,
                    Id = x.Id,
                    Mail = x.Mail,
                }).TryPagination(request.PageCount, request.PageNumber).ToListAsync();

                response.TotalCount = await query.CountAsync();
                response.UserList = data;

                return GenericResponse<GetAllUserResponse>.Success(200, response);
            }
            catch (Exception ex)
            {
                _logHelper.LogError(ex);
                return GenericResponse<GetAllUserResponse>.Error(500, ex.Message);
            }
        }
    }
}
