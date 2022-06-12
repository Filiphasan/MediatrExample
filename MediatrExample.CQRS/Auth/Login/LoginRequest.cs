namespace MediatrExample.CQRS.Auth.Login
{
    public class LoginRequest : BaseCommand<LoginResponse>
    {
        public string Mail { get; set; }
        public string Password { get; set; }
    }
}
