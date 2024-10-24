using AutoMapper;
using Library.BusinessAccess.Models.Book;
using Library.Domain;

namespace Library.BusinessAccess.MappingProfiles
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