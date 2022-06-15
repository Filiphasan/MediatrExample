using System.Text.Json.Serialization;

namespace MediatrExample.CQRS.User.UpdatePwUser
{
    public class UpdatePwUserRequest : BaseCommand<UpdatePwUserResponse>
    {
        [JsonIgnore]
        public int UserId { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
