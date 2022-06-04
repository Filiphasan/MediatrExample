using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatrExample.Shared.Data
{
    public class EntityBase : IEntity
    {
        public EntityBase()
        {
            CreatedTime = DateTime.Now;
        }

        public Nullable<DateTime> UpdatedTime { get; set; }
        public Nullable<DateTime> CreatedTime { get; set; }
    }
}
