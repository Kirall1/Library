using AutoMapper;
using Library.BusinessAccess.Models.Author;
using Library.Domain;

namespace Library.BusinessAccess.MappingProfiles
{
    public class AuthorProfile : Profile, IMapperMarker
    {
        public AuthorProfile()
        {
            CreateMap<Author, AuthorResponseDto>();

            CreateMap<Author, AuthorCreateResponseDto>();

            CreateMap<AuthorCreateDto, Author>();

            CreateMap<AuthorUpdateDto, Author>();
        }
    }
}