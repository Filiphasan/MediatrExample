namespace MediatrExample.CQRS.User.AddUser
{
    public class AddUserRequest : BaseCommand<AddUserResponse>
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Gsm { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
