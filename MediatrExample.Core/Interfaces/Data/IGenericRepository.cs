using MediatrExample.Shared.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MediatrExample.Core.Interfaces.Data
{
    public interface IGenericRepository<TEntity> where TEntity : class, IEntity, new()
    {
        Task<TEntity> GetByIdAsync(object id, bool hasTrack = false);
        TEntity GetById(object id, bool hasTrack = false);
        Task<IEnumerable<TEntity>> GetAllAsync();
        IEnumerable<TEntity> GetAll();
        IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate);
        IQueryable<TEntity> JoinWhere(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes);
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
