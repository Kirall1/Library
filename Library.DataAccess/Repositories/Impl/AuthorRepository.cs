using Library.BusinessObject;

namespace Library.DataAccess.Repositories.Impl
{
    public class AuthorRepository : BaseRepository<Author>, IAuthorRepository
    {
        public AuthorRepository(DatabaseContext context) : base(context) { }
    }
}
