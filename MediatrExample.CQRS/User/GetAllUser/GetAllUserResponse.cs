using MediatrExample.Shared.DataModels.User.GetAllUser;
using MediatrExample.Shared.RequestResponse.Pagination;

namespace MediatrExample.CQRS.User.GetAllUser
{
    public class GetAllUserResponse : PaginationResponse
    {
        public IEnumerable<UserDataModel>? UserList { get; set; }
    }
}
