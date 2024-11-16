using AutoMapper;
using ECommerce.Business.Interfaces;
using ECommerce.Models.Dtos.CategoryDtos;
using ECommerce.WebAPI.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly ILogger<CategoryController> _logger;

        public CategoryController(ICategoryService categoryService, ILogger<CategoryController> logger)
        {
            _categoryService = categoryService;
            _logger = logger;
        }

        /// <summary>
        /// Tüm kategorileri getirir.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();

            if (categories == null || !categories.Any())
            {
                _logger.LogWarning("Kategoriler bulunamadı.");
                return ResponseHelper.NotFound("Kategoriler bulunamadı.");
            }

            _logger.LogInformation("Tüm kategoriler başarıyla getirildi.");
            return ResponseHelper.Ok("Kategoriler başarıyla getirildi.", categories);
        }

        /// <summary>
        /// Kategori id'ye göre kategoriyi getirir.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);

            if (category == null)
            {
                _logger.LogWarning("Kategori bulunamadı. Id: {CategoryId}", id);
                return ResponseHelper.NotFound($"Kategori Id {id} bulunamadı.");
            }

            _logger.LogInformation("Kategori başarıyla getirildi. Id: {CategoryId}", id);
            return ResponseHelper.Ok("Kategori başarıyla getirildi.", category);
        }

        /// <summary>
        /// Yeni kategori ekler.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> AddCategory([FromBody] CategoryDto categoryDto)
        {
            if (categoryDto == null)
            {
                _logger.LogWarning("Kategori verisi null gönderildi.");
                return ResponseHelper.BadRequest("Kategori verisi gereklidir.");
            }

            await _categoryService.AddCategoryAsync(categoryDto);
            _logger.LogInformation("Kategori başarıyla eklendi. Id: {CategoryId}", categoryDto.Id);

            return ResponseHelper.CreatedAtAction(nameof(GetCategoryById), new { id = categoryDto.Id },
                "Kategori başarıyla eklendi.", categoryDto);
        }

        /// <summary>
        /// Kategori günceller.
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] CategoryDto categoryDto)
        {
            if (categoryDto == null || id != categoryDto.Id)
            {
                _logger.LogWarning("Kategori Id uyuşmazlığı. Gönderilen Id: {Id}, DTO Id: {DtoId}", id,
                    categoryDto?.Id);
                return ResponseHelper.BadRequest("Kategori Id uyuşmazlığı.");
            }

            await _categoryService.UpdateCategoryAsync(categoryDto);
            _logger.LogInformation("Kategori başarıyla güncellendi. Id: {CategoryId}", id);

            return ResponseHelper.Ok("Kategori başarıyla güncellendi.", categoryDto);
        }

        /// <summary>
        /// Kategori siler.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            await _categoryService.DeleteCategoryAsync(id);
            _logger.LogInformation("Kategori başarıyla silindi. Id: {CategoryId}", id);

            return ResponseHelper.NoContent();
        }

        /// <summary>
        /// Kategori ve ürünleri birlikte getirir.
        /// </summary>
        [HttpGet("WithProducts")]
        public async Task<IActionResult> GetCategoriesWithProducts()
        {
            var categories = await _categoryService.GetCategoriesWithProductsAsync();

            if (categories == null || !categories.Any())
            {
                _logger.LogWarning("Kategori ve ürünler bulunamadı.");
                return ResponseHelper.NotFound("Kategori ve ürünler bulunamadı.");
            }

            _logger.LogInformation("Kategori ve ürünler başarıyla getirildi.");
            return ResponseHelper.Ok("Kategori ve ürünler başarıyla getirildi.", categories);
        }
    }
}