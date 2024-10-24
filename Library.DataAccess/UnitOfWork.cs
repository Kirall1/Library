using Library.DataAccess.Repositories.Impl;
using Library.Domain.Repositories;
using Library.Shared;

namespace Library.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private DatabaseContext _dbContext;
        private IBookRepository _bookRepository;
        private IAuthorRepository _authorRepository;

        public UnitOfWork(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IBookRepository Books
        {
            get
            {
                if (_bookRepository == null)
                    _bookRepository = new BookRepository(_dbContext);
                return _bookRepository;
            }
        }

        public IAuthorRepository Authors
        {
            get
            {
                if (_authorRepository == null)
                    _authorRepository = new AuthorRepository(_dbContext);
                return _authorRepository;
            }
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        private bool _disposed = false;
        public virtual void Dispose(bool disposing)
        {
            if (!_disposed) 
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
                this._disposed = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
