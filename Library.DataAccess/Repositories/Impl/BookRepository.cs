using Library.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.DataAccess.Repositories.Impl
{
    public class BookRepository : BaseRepository<Book>, IBookRepository
    {
        public BookRepository(DatabaseContext context) : base(context) { }
        public async Task<IEnumerable<Book>> GetAllBooksGroupByTitleAndAuthorAsync(CancellationToken cancellationToken)
        {
            return await _dbSet.
                GroupBy(x => new { x.Title, x.Author }).
                SelectMany(g => g).
                ToListAsync(cancellationToken);
        }
    }
}
