using AutoMapper;
using Library.Business.Models.Book;
using Library.DataAccess.Entities;

namespace Library.Business.MappingProfiles
{
    public class BookProfile : Profile, IMapperMarker
    {
        public BookProfile()
        {
            CreateMap<Book, BookBaseResponseDto>()
                .ForMember(dest => dest.AuthorName,
                    opt => opt.MapFrom(src => src.Author.Name + " " + src.Author.Surname));

            CreateMap<Book, BookDetailedResponseDto>()
                .ForMember(dest => dest.AuthorName,
                    opt => opt.MapFrom(src => src.Author.Name + " " + src.Author.Surname));

            CreateMap<Book, BookFullResponseDto>()
                .ForMember(dest => dest.AuthorName,
                    opt => opt.MapFrom(src => src.Author.Name + " " + src.Author.Surname));

            CreateMap<Book, BookCreateResponseDto>();

            CreateMap<BookCreateDto, Book>();

            CreateMap<BookUpdateDto, Book>();
        }
    }
}