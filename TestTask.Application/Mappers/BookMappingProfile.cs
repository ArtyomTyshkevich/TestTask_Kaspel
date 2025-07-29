using AutoMapper;
using TestTask.Application.DTOs;
using TestTask.Domain.Entities;

namespace TestTask.Application.Mapping
{
    public class BookMappingProfile : Profile
    {
        public BookMappingProfile()
        {
            CreateMap<Book, BookDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Author))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.ReleaseDate, opt => opt.MapFrom(src => src.ReleaseDate))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price));

            CreateMap<BookDTO, Book>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Author))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.ReleaseDate, opt => opt.MapFrom(src => src.ReleaseDate))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price));
        }
    }
}
