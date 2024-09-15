using Library.DataAccess.Entities;
using System.Linq.Expressions;

namespace Library.DataAccess.Repositories
{
    public interface IBaseRepository<TEntity> : IDisposable where TEntity : BaseEntity 
    {
        Task<TEntity> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<TEntity> GetFirstByPredicateAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
        Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<IEnumerable<TEntity>> GetAllByPredicateAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
        Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken);
        TEntity Update(TEntity entitty);
        TEntity Delete(TEntity entity);
    }
}
