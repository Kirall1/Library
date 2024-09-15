using Library.DataAccess.Entities;

namespace Library.Business.Dtos.Book
{
    public class BookCreateDto
    {
        public string Isbn { get; set; }
        public string Title { get; set; }
        public Genre Genre { get; set; }
        public string Description { get; set; }
        public DataAccess.Entities.Author Author { get; set; }
        //public string? Image { get; set; }
    }
}
