namespace MediatrExample.CQRS.User.ForgotPwUser
{
    public class ForgotPwUserRequest : BaseCommand<ForgotPwUserResponse>
    {
        public string Mail { get; set; }
    }
}
