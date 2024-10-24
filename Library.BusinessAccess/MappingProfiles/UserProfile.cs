using AutoMapper;
using Library.BusinessAccess.Models.User;
using Library.Shared;

namespace Library.BusinessAccess.MappingProfiles
{
    public class UserProfile : Profile, IMapperMarker
    {
        public UserProfile()
        {
            CreateMap<ApplicationUser, UserCreateResponseDto>();

            CreateMap<UserCreateDto, ApplicationUser>();

            CreateMap<UserLoginDto, ApplicationUser>();
        }
    }
}