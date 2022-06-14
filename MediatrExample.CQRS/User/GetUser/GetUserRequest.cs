using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatrExample.CQRS.User.GetUser
{
    public class GetUserRequest : BaseQuery<GetUserResponse>
    {
        public int UserId { get; set; }
    }
}
