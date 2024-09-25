using Library.Business.Models;
using Library.Business.Models.User;

namespace Library.Business.Services
{
    public interface IUserService
    {
        public Task<TokenApiModel> Login(UserLoginDto userLoginDto);
        public Task<UserCreateResponseDto> Register(UserCreateDto userCreateDtoDto, CancellationToken cancellationToken);
    }
}