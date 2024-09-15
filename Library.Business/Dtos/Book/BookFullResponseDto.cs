using Library.DataAccess.Entities;

namespace Library.Business.Dtos.Book
{
    public class BookFullResponseDto : BookDetailedResponseDto
    {
        public DateTime BookTakenTime { get; set; }
        public DateTime BookReturnTime { get; set; }
        public User? CurrentUser { get; set; }
    }
}
