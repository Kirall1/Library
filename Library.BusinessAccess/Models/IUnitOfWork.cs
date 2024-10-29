using Library.Domain.Repositories;

namespace Library.BusinessAccess.Models
{
    public interface IUnitOfWork : IDisposable
    {
        public Task SaveChangesAsync(CancellationToken cancellationToken);
        public IBookRepository Books { get; }
        public IAuthorRepository Authors { get; }
    }
}
