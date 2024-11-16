using AutoMapper;
using ECommerce.Entities.Entities.Concrete;
using ECommerce.Models.Dtos.ProductDtos;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ECommerce.Business.Mappings.AutoMapper;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        // ProductDto <-> Product dönüşümü
        CreateMap<ProductDto, Product>().ReverseMap()
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category != null ? src.Category.Name : null)); // Kategori adı güvenli şekilde eşleniyor

        // Alternatif: Eğer daha fazla özelleştirme gerekiyorsa, Custom Resolver eklenebilir
        //.ForMember(dest => dest.CategoryName, opt => opt.MapFrom<CategoryNameResolver>());
    }

}