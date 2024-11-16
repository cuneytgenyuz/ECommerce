using AutoMapper;
using ECommerce.Entities.Entities.Concrete;
using ECommerce.Models.Dtos.CategoryDtos;

namespace ECommerce.Business.Mappings.AutoMapper;

public class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        // CategoryDto <-> Category dönüşümü
        CreateMap<CategoryDto, Category>().ReverseMap(); // Ters yönlü maplemeyi tek satırda yaptık

        // Category -> CategoryWithProductsDto dönüşümü
        CreateMap<Category, CategoryWithProductsDto>()
            .ForMember(dest => dest.Products, opt => opt.MapFrom(src =>
                src.Products ?? new List<Product>())); // Null kontrolü ile güvenli bir mapleme

        // Optional: Daha karmaşık bir dönüşüm gerekirse Custom Logic eklenebilir.
        // Örnek: Özel bir işlem gerektiğinde dönüşüm sırasında çözümleyici (Resolver) kullanabilirsiniz
        //.ForMember(dest => dest.Products, opt => opt.MapFrom<ProductListResolver>());
    }

}
