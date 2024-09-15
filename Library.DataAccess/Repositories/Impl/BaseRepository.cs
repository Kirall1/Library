using Library.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

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

        public async Task<TEntity> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var entity = await _dbSet.Where(o => o.Id == id).FirstOrDefaultAsync(cancellationToken);

            return entity;
        }

        public async Task<TEntity> GetFirstByPredicateAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            var entity = await _dbSet.FirstOrDefaultAsync(predicate, cancellationToken);

            return entity;
        }

        public async Task<IEnumerable<TEntity>> GetAllByPredicateAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            var entities = await _dbSet.Where(predicate).ToListAsync(cancellationToken);

            return entities;
        }

        public TEntity Update(TEntity entity)
        {
            _dbSet.Update(entity);

            return entity;
        }

        public TEntity Delete(TEntity entity)
        {
            var removedModel = _dbSet.Remove(entity).Entity;

            return removedModel;
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
