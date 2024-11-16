using AutoMapper;
using ECommerce.Business.Interfaces;
using ECommerce.Core.Interfaces;
using ECommerce.Entities.Entities.Concrete;
using ECommerce.Models.Dtos.CategoryDtos;
using Microsoft.EntityFrameworkCore;


namespace ECommerce.Business.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // Tüm kategorileri getiren metot
        public async Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync()
        {
            var categories = await _unitOfWork.Categories.GetAllAsync();
            var categoryDtos = _mapper.Map<IEnumerable<CategoryDto>>(categories);
            return categoryDtos;
        }

        // Belirli bir kategoriyi ID ile getiren metot
        public async Task<CategoryDto> GetCategoryByIdAsync(int id)
        {
            var category = await _unitOfWork.Categories.GetByIdAsync(id);
            if (category == null)
            {
                throw new KeyNotFoundException($"KategoriId {id} ile kategori bulunamadı.");
            }

            var categoryDto = _mapper.Map<CategoryDto>(category);
            return categoryDto;
        }

        // Yeni bir kategori ekleyen metot
        public async Task AddCategoryAsync(CategoryDto categoryDto)
        {
            if (categoryDto == null)
            {
                throw new ArgumentNullException(nameof(categoryDto), "Kategori verisi boş olamaz.");
            }

            var category = _mapper.Map<Category>(categoryDto);
            await _unitOfWork.Categories.AddAsync(category);
            await _unitOfWork.CompleteAsync();
        }

        // Var olan bir kategoriyi güncelleyen metot
        public async Task UpdateCategoryAsync(CategoryDto categoryDto)
        {
            if (categoryDto == null)
            {
                throw new ArgumentNullException(nameof(categoryDto), "Kategori verisi boş olamaz.");
            }

            var category = await _unitOfWork.Categories.GetByIdAsync(categoryDto.Id);
            if (category == null)
            {
                throw new KeyNotFoundException($"Kategori Id {categoryDto.Id} ile kategori bulunamadı.");
            }

            // Güncellenmiş verileri entity'ye map et
            _mapper.Map(categoryDto, category);
            await _unitOfWork.Categories.UpdateAsync(category);
            await _unitOfWork.CompleteAsync();
        }

        // Bir kategoriyi silen metot
        public async Task DeleteCategoryAsync(int id)
        {
            var category = await _unitOfWork.Categories.GetByIdAsync(id);
            if (category == null)
            {
                throw new KeyNotFoundException($"Kategori Id {id} ile kategori bulunamadı.");
            }

            await _unitOfWork.Categories.DeleteAsync(id);
            await _unitOfWork.CompleteAsync();
        }

        // Kategorilerle birlikte ürünleri getiren metot
        public async Task<IEnumerable<CategoryWithProductsDto>> GetCategoriesWithProductsAsync()
        {
            var categoriesQuery = _unitOfWork.Categories.AsQueryable();

            var categoriesWithProducts = await categoriesQuery
                .Include(c => c.Products) 
                .ToListAsync();

            var categoryWithProductsDtos = _mapper.Map<IEnumerable<CategoryWithProductsDto>>(categoriesWithProducts);
            return categoryWithProductsDtos;
        }


    }
}
