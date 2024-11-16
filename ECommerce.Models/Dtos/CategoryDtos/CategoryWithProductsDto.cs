using ECommerce.Models.Dtos.ProductDtos;

namespace ECommerce.Models.Dtos.CategoryDtos;

public class CategoryWithProductsDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<ProductDto> Products { get; set; }
}