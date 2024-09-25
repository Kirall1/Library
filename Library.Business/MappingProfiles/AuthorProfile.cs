using AutoMapper;
using Library.Business.Models.Author;
using Library.DataAccess.Entities;

namespace Library.Business.MappingProfiles
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