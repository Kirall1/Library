using Library.DataAccess.Entities;

namespace Library.DataAccess.Repositories.Impl
{
    public class GenreRepository : BaseRepository<Genre>, IGenreRepository
    {
        public GenreRepository(DatabaseContext context) : base(context) { }
    }
}
