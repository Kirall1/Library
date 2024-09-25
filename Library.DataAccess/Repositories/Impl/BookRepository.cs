﻿using Library.DataAccess.Entities;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Library.DataAccess.Repositories.Impl
{
    public class BookRepository : BaseRepository<Book>, IBookRepository
    {
        public BookRepository(DatabaseContext context) : base(context) { }

        public async Task<Book?> GetByIsbnAsync(string isbn, CancellationToken cancellationToken)
        {
            return await _dbSet.Where(b => b.Isbn == isbn).FirstOrDefaultAsync(cancellationToken);
        }
    }
}
