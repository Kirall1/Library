using Library.DataAccess.Entities;
using Library.DataAccess.Repositories.Impl;
using Microsoft.AspNetCore.Identity;

namespace Library.DataAccess
{
    public interface IUnitOfWork : IDisposable
    {
        public Task SaveChangesAsync(CancellationToken cancellationToken);
        public BookRepository Books { get; }
        public AuthorRepository Authors { get; }

    }
}
