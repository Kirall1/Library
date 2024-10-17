using Library.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Library.BusinessObject;

namespace Library.DataAccess.Repositories.Impl
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity>, IDisposable where TEntity : BaseEntity
    {
        protected readonly DatabaseContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        protected BaseRepository(DatabaseContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            var addedModel = (await _dbSet.AddAsync(entity, cancellationToken)).Entity;

            return addedModel;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _dbSet.ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<TEntity>> GetByPageAsync(int page, int pageSize,
            CancellationToken cancellationToken = default)
        {
            return await _dbSet.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);
        }

        public async Task<TEntity> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var entity = await _dbSet.Where(o => o.Id == id).FirstOrDefaultAsync(cancellationToken);

            return entity;
        }

        public async Task<TEntity?> GetFirstByPredicateAsync(Expression<Func<TEntity, bool>> predicate,
            CancellationToken cancellationToken = default)
        {
            return await _dbSet.FirstOrDefaultAsync(predicate, cancellationToken);
        }

        public async Task<IEnumerable<TEntity>> GetAllByPredicateAsync(Expression<Func<TEntity, bool>> predicate,
            CancellationToken cancellationToken = default)
        {
            return await _dbSet.Where(predicate).ToListAsync(cancellationToken);
        }

        public TEntity Update(TEntity entity)
        {
            _dbSet.Update(entity);

            return entity;
        }

        public TEntity Delete(TEntity entity)
        {
            return _dbSet.Remove(entity).Entity;
        }


        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}
