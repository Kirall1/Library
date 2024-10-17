using AutoMapper;
using Library.DataAccess.Entities;
using Library.BusinessAccess.Models.User;

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