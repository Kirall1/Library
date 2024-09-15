using Library.Business.Models.Book;

namespace Library.Business.Dtos.Book
{
    public class BookUpdateDto : BookBaseResponseDto
    {
        public string Description { get; set; }
    }
}
