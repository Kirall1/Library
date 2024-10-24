namespace Library.Domain.Repositories
{
    public interface IBookRepository : IBaseRepository<Book>
    {
        public Task<Book?> GetByIsbnAsync(string isbn, CancellationToken cancellationToken);
    }
}
