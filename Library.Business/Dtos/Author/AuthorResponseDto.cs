using Library.Business.Models;

namespace Library.Business.Dtos.Author
{
    public class AuthorResponseDto : BaseResponseDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateOnly BirthDate { get; set; }
        public string Country {  get; set; }
    }
}
