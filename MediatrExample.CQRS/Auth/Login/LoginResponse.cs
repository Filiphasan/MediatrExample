using MediatrExample.Shared.DataModels.User.GetAllUser;

namespace MediatrExample.CQRS.Auth.Login
{
    public class LoginResponse
    {
        public string AccessToken { get; set; }
        public UserDataModel User { get; set; }
    }
}
