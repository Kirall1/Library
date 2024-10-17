using Library.BusinessAccess.Models;
using Library.BusinessAccess.Models.User;

namespace Library.BusinessAccess.Services
{
    public interface IUserService
    {
        public Task<TokenApiModel> Login(UserLoginDto userLoginDto);
        public Task<UserCreateResponseDto> Register(UserCreateDto userCreateDtoDto, CancellationToken cancellationToken);
    }
}