using System.ComponentModel.DataAnnotations.Schema;

namespace MediatrExample.Shared.Data
{
    public class EntityBase : IEntity
    {
        public EntityBase()
        {
            CreatedTime = DateTime.Now;
        }

        [Column("UPDATED_TIME")]
        public Nullable<DateTime> UpdatedTime { get; set; }
        [Column("CREATED_TIME")]
        public Nullable<DateTime> CreatedTime { get; set; }
    }
}
