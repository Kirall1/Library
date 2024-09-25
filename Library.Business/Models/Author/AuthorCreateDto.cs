namespace Library.Business.Models.Author
{
    public class AuthorCreateDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateOnly BirthDate { get; set; }
        public string Country { get; set; }
    }

    public class AuthorCreateResponseDto : BaseResponseDto { };
}
