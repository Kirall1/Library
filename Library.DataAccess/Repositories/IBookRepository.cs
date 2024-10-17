using Library.BusinessObject;

namespace Library.DataAccess.Repositories
{
    public interface IBookRepository : IBaseRepository<Book>
    {
        public Task<Book?> GetByIsbnAsync(string isbn, CancellationToken cancellationToken);
    }
}
