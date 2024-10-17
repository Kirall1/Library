namespace Library.BusinessAccess.Models.User
{
    public class UserCreateDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class UserCreateResponseDto : BaseResponseDto { };
}