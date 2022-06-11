using MediatrExample.Shared.DataModels.User.GetAllUser;

namespace MediatrExample.CQRS.User.AddUser
{
    public class AddUserResponse : UserDataModel
    {
        public string AccessToken { get; set; } = string.Empty;
    }
}
