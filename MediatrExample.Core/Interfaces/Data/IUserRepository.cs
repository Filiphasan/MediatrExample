using MediatrExample.Core.Entities;

namespace MediatrExample.Core.Interfaces.Data
{
    public interface IUserRepository : IGenericRepository<User>
    {
        IQueryable<User> GetUserList(string searchQuery);
    }
}
