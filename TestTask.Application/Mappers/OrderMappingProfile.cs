using AutoMapper;
using TestTask.Application.DTOs;
using TestTask.Domain.Entities;

namespace TestTask.Application.Mapping
{
    public class OrderMappingProfile : Profile
    {
        public OrderMappingProfile()
        {
            CreateMap<Order, OrderDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.CreatedDate))
                .ForMember(dest => dest.Books, opt => opt.MapFrom(src => src.Books))
                .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.TotalPrice));

            CreateMap<OrderDTO, Order>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.CreatedDate))
                .ForMember(dest => dest.Books, opt => opt.MapFrom(src => src.Books))
                .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.TotalPrice));
            
            CreateMap<OrderRequest, Order>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Books, opt => opt.Ignore())
            .ForMember(dest => dest.TotalPrice, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(_ => DateTime.UtcNow));
        }
    }
}
