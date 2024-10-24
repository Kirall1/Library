using System.Security.Claims;
using AutoMapper;
using Library.BusinessAccess.Models;
using Library.BusinessAccess.Models.User;
using Library.Shared;
using Microsoft.AspNetCore.Identity;

namespace Library.BusinessAccess.Services.Impl
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public UserService(UserManager<ApplicationUser> userManager, ITokenService tokenService,
            IMapper mapper)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _mapper = mapper;
        }

        public async Task<TokenApiModel> Login(UserLoginDto userLoginDto)
        {
            var user = await _userManager.FindByNameAsync(userLoginDto.Username);

            if (user == null || !await _userManager.CheckPasswordAsync(user, userLoginDto.Password))
            {
                throw new Exception("Invalid username or password.");
            }

            var refreshToken = _tokenService.GenerateRefreshToken();
            var token = new TokenApiModel()
            {
                AccessToken = _tokenService.GenerateAccessToken(await _userManager.GetClaimsAsync(user))
            };
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);
            await _userManager.UpdateAsync(user);

            return token;
        }

        public async Task<UserCreateResponseDto> Register(UserCreateDto userCreateDto, CancellationToken cancellationToken)
        {
            var existingUser = await _userManager.FindByNameAsync(userCreateDto.UserName);

            if (existingUser != null)
            {
                throw new Exception("Username already exists.");
            }

            var newUser = _mapper.Map<ApplicationUser>(userCreateDto);

            await _userManager.CreateAsync(newUser, userCreateDto.Password);
            var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, newUser.UserName),
                    new Claim(ClaimTypes.NameIdentifier, newUser.Id.ToString()),
                    new Claim(ClaimTypes.Email, newUser.Email),
                    new Claim(ClaimTypes.Role, "User")
                };
            var addedUser = await _userManager.FindByNameAsync(newUser.UserName);
            await _userManager.AddClaimsAsync(addedUser, claims);

            return _mapper.Map<UserCreateResponseDto>(newUser);
        }

    }
}