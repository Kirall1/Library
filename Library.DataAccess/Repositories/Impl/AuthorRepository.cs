using Library.DataAccess.Entities;

namespace Library.DataAccess.Repositories.Impl
{
    public class AuthorRepository : BaseRepository<Author>, IAuthorRepository
    {
        public AuthorRepository(DatabaseContext context) : base(context) { }
    }
}
