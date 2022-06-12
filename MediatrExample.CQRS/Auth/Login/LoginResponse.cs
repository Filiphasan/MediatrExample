using MediatrExample.Shared.DataModels.User.GetAllUser;

namespace MediatrExample.CQRS.Auth.Login
{
    public class LoginResponse : UserDataModel
    {
        public string AccessToken { get; set; }
    }
}
