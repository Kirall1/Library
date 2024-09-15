namespace Library.DataAccess
{
    internal interface IUnitOfWork : IDisposable
    {
        public Task SaveChangesAsync(CancellationToken cancellationToken);
    }
}
