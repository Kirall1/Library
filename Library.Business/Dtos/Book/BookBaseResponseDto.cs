using Library.DataAccess.Entities;

namespace Library.Business.Models.Book
{
    public class BookBaseResponseDto : BaseResponseDto
    {
        public string Isbn { get; set; }
        public string Title { get; set; }
        public Genre Genre { get; set; }
        public string Author { get; set; }
        //add image
        
    }
}
