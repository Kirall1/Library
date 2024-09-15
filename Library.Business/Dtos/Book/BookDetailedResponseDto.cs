using Library.Business.Models.Book;

namespace Library.Business.Dtos.Book
{
    public class BookDetailedResponseDto : BookBaseResponseDto
    {
        public string Description { get; set; }
    }
}
