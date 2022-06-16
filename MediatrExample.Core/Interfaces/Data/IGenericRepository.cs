using MediatrExample.Shared.Data;
using System.Linq.Expressions;

namespace MediatrExample.Core.Interfaces.Data
{
    public interface IGenericRepository<TEntity> where TEntity : class, IEntity, new()
    {
        Task<TEntity> GetByIdAsync(object id, bool hasTrack = false);
        TEntity GetById(object id, bool hasTrack = false);
        Task<IEnumerable<TEntity>> GetAllAsync(bool hasTrack = false);
        IEnumerable<TEntity> GetAll(bool hasTrack = false);
        IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate, bool hasTrack = false);
        IQueryable<TEntity> JoinWhere(Expression<Func<TEntity, bool>> predicate, bool hasTrack, params Expression<Func<TEntity, object>>[] includes);
        Task<TEntity> InsertAsyncReturn(TEntity entity);
        Task InsertAsync(TEntity entity);
        void Insert(TEntity entity);
        TEntity InsertReturn(TEntity entity);
        void Delete(TEntity entity);
        void Delete(object id);
        TEntity Update(TEntity entity);
        void SaveChanges();
        Task SaveChangesAsync();
    }
}
