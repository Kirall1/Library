using Library.DataAccess.Entities;

namespace Library.DataAccess.Repositories
{
    public interface IBookRepository : IBaseRepository<Book>
    {
        public Task<IEnumerable<Book>> GetAllBooksGroupByTitleAndAuthorAsync(CancellationToken cancellationToken);
    }
}
