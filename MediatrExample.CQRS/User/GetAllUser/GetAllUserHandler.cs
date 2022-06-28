using EnLock;
using MediatR;
using MediatrExample.Core.Interfaces.Data;
using MediatrExample.Core.Interfaces.Service;
using MediatrExample.Core.Interfaces.Service.CacheService;
using MediatrExample.Service.Utilities;
using MediatrExample.Shared.Constants;
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
        private readonly IRedisCacheService _redisCacheService;
        private readonly CacheKeyUtility CacheKeyUtility;

        public GetAllUserHandler(IHttpContextAccessor httpContextAccessor, IEnumerable<FluentValidation.IValidator<GetAllUserRequest>> validators, ILogHelper<GetAllUserHandler> logHelper, IUserRepository userRepository, IRedisCacheService redisCacheService, CacheKeyUtility cacheKeyUtility) : base(httpContextAccessor, validators, logHelper)
        {
            _userRepository = userRepository;
            _redisCacheService = redisCacheService;
            CacheKeyUtility = cacheKeyUtility;
        }

        public async Task<GenericResponse<GetAllUserResponse>> Handle(GetAllUserRequest request, CancellationToken cancellationToken)
        {
            await CheckValidate(request);
            try
            {
                var response = new GetAllUserResponse();

                string cacheKey = CacheKeyUtility.GetCacheKey(CacheKeys.GetUserListKey);

                var cacheUserList= await _redisCacheService.GetAsync<IEnumerable<UserDataModel>>(cacheKey);

                if (cacheUserList != null && cacheUserList.Any())
                {
                    response.UserList = cacheUserList;
                    response.TotalCount = cacheUserList.Count();
                    return GenericResponse<GetAllUserResponse>.Success(200, response);
                }

                var query = _userRepository.GetUserList(request.Query);

                var data = await query.Select(x => new UserDataModel
                {
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Gsm = x.Gsm,
                    Id = x.Id,
                    Mail = x.Mail,
                }).TryPagination(request.PageCount, request.PageNumber).ToListWithNoLockAsync();

                await _redisCacheService.SetAsync(cacheKey, data);

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
