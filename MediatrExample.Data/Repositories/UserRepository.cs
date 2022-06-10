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

        public IQueryable<User> GetUserList(string searchQuery)
        {
            var query = (from user in _context.Users
                         select user).AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                query = query.Where(x => x.Mail.Contains(searchQuery));
            }
            return query;
        }
    }
}
