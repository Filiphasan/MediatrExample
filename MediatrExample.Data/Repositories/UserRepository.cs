using MediatrExample.Core.Entities;
using MediatrExample.Core.Interfaces.Data;
using MediatrExample.Data.Context;

namespace MediatrExample.Data.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context)
        {
        }
    }
}
