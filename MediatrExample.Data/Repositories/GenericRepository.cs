using MediatrExample.Core.Interfaces.Data;
using MediatrExample.Data.Context;
using MediatrExample.Shared.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MediatrExample.Data.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class, IEntity, new()
    {
        protected readonly AppDbContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public async Task AttachUpdateAsync(TEntity entity, params string[] propsName)
        {
            _dbSet.Attach(entity);
            foreach (var prop in propsName)
            {
                _context.Entry(entity).Property(prop).IsModified = true;
            }
            await _context.SaveChangesAsync();
        }

        public async Task AttachUpdateAsync(TEntity entity, params Expression<Func<TEntity, object>>[] expressions)
        {
            _dbSet.Attach(entity);
            foreach (var expression in expressions)
            {
                _context.Entry(entity).Property(expression).IsModified = true;
            }
            await _context.SaveChangesAsync();
        }

        public void Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
            _context.SaveChanges();
        }

        public void Delete(object id)
        {
            var entity = _dbSet.Find(id);
            if (entity != null)
            {
                _context.Entry(entity).State = EntityState.Detached;
            }
            _dbSet.Remove(entity);
            _context.SaveChanges();
        }

        public IEnumerable<TEntity> GetAll(bool hasTrack = false)
        {
            if (hasTrack)
            {
                return _dbSet.ToList();
            }
            else
            {
                return _dbSet.AsNoTracking().ToList();
            }
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(bool hasTrack = false)
        {
            if (hasTrack)
            {
                return await _dbSet.ToListAsync();
            }
            else
            {
                return await _dbSet.AsNoTracking().ToListAsync();
            }
        }

        public TEntity GetById(object id, bool hasTrack = false)
        {
            var entity = _dbSet.Find(id);
            if (entity != null && !hasTrack)
            {
                _context.Entry(entity).State = EntityState.Detached;
            }
            return entity;
        }

        public async Task<TEntity> GetByIdAsync(object id, bool hasTrack = false)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null && !hasTrack)
            {
                _context.Entry(entity).State = EntityState.Detached;
            }
            return entity;
        }

        public void Insert(TEntity entity)
        {
            _dbSet.Add(entity);
            _context.SaveChanges();
        }

        public async Task InsertAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<TEntity> InsertAsyncReturn(TEntity entity)
        {
            var data = await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return data.Entity;
        }

        public TEntity InsertReturn(TEntity entity)
        {
            var data = _dbSet.Add(entity);
            _context.SaveChanges();
            return data.Entity;
        }

        public IQueryable<TEntity> JoinWhere(Expression<Func<TEntity, bool>> predicate, bool hasTrack, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = _dbSet;
            if (!hasTrack)
            {
                query = query.AsNoTracking();
            }
            query = query.Where(predicate);
            if (includes.Any())
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }
            return query;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public TEntity Update(TEntity entity)
        {
            var data = _dbSet.Update(entity);
            _context.SaveChanges();
            return data.Entity;
        }

        public IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate, bool hasTrack = false)
        {
            var query = _dbSet.Where(predicate);
            if (!hasTrack)
            {
                query = query.AsNoTracking();
            }
            return query;
        }
    }
}
