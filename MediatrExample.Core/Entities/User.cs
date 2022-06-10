using MediatrExample.Shared.Data;

namespace MediatrExample.Core.Entities
{
    public class User : EntityBase
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Mail { get; set; } = string.Empty;
        public string Gsm { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
    }
}
