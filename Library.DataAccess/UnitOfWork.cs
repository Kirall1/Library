using Library.DataAccess.Repositories.Impl;
using Microsoft.EntityFrameworkCore;

namespace Library.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private DatabaseContext _dbContext;
        private BookRepository _bookRepository;
        private AuthorRepository _authorRepository;
        private GenreRepository _genreRepository;

        public UnitOfWork(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public BookRepository Books
        {
            get
            {
                if (_bookRepository == null)
                    _bookRepository = new BookRepository(_dbContext);
                return _bookRepository;
            }
        }

        public AuthorRepository Authors
        {
            get
            {
                if (_authorRepository == null)
                    _authorRepository = new AuthorRepository(_dbContext);
                return _authorRepository;
            }
        }

        public GenreRepository Genres
        {
            get
            {
                if (_genreRepository == null)
                    _genreRepository = new GenreRepository(_dbContext);
                return (_genreRepository);
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
