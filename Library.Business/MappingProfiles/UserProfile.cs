using AutoMapper;
using Library.DataAccess.Entities;
using Library.Business.Models.User;

namespace Library.Business.MappingProfiles
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