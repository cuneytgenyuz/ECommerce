using ECommerce.Models.Dtos.CategoryDtos;

namespace ECommerce.Business.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync();
        Task<CategoryDto> GetCategoryByIdAsync(int id);
        Task AddCategoryAsync(CategoryDto categoryDto);
        Task UpdateCategoryAsync(CategoryDto categoryDto);
        Task DeleteCategoryAsync(int id);
        Task<IEnumerable<CategoryWithProductsDto>> GetCategoriesWithProductsAsync();
    }
}
