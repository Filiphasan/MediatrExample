using MediatrExample.Shared.RequestResponse.Pagination;

namespace MediatrExample.CQRS.User.GetAllUser
{
    public class GetAllUserRequest : BasePagingQuery<GetAllUserResponse>
    {
        public string Query { get; set; } = string.Empty;
    }
}
