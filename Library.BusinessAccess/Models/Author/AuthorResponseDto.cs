namespace Library.BusinessAccess.Models.Author
{
    public class AuthorResponseDto : BaseResponseDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateOnly BirthDate { get; set; }
        public string Country { get; set; }
    }
}
