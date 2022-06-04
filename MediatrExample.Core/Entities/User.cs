using MediatrExample.Shared.Data;

namespace MediatrExample.Core.Entities
{
    public class User : EntityBase
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Mail { get; set; }
        public string Gsm { get; set; }
    }
}
