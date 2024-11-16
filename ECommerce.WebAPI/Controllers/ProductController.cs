using ECommerce.Business.Interfaces;
using ECommerce.Core.Helpers;
using ECommerce.Models.Dtos.ProductDtos;
using ECommerce.WebAPI.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ILogger<ProductController> _logger;

        public ProductController(IProductService productService, ILogger<ProductController> logger)
        {
            _productService = productService;
            _logger = logger;
        }

        /// <summary>
        /// Tüm ürünleri getirir.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productService.GetAllProductsAsync();

            if (products == null || !products.Any())
            {
                _logger.LogWarning("Ürünler bulunamadı.");
                return ResponseHelper.NotFound("Ürünler bulunamadı.");
            }

            _logger.LogInformation("Tüm ürünler başarıyla getirildi.");
            return ResponseHelper.Ok("Ürünler başarıyla getirildi.", products);
        }

        /// <summary>
        /// Ürün id'ye göre ürünü getirir.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);

            if (product == null)
            {
                _logger.LogWarning("Ürün bulunamadı. Id: {ProductId}", id);
                return ResponseHelper.NotFound($"Ürün Id {id} bulunamadı.");
            }

            _logger.LogInformation("Ürün başarıyla getirildi. Id: {ProductId}", id);
            return ResponseHelper.Ok("Ürün başarıyla getirildi.", product);
        }

        /// <summary>
        /// Yeni ürün ekler.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] ProductDto productDto)
        {
            if (productDto == null)
            {
                _logger.LogWarning("Ürün verisi null gönderildi.");
                return ResponseHelper.BadRequest("Ürün verisi gereklidir.");
            }

            await _productService.AddProductAsync(productDto);
            _logger.LogInformation("Ürün başarıyla eklendi. Id: {ProductId}", productDto.Id);

            return ResponseHelper.CreatedAtAction(nameof(GetProductById), new { id = productDto.Id }, "Ürün başarıyla eklendi.", productDto);
        }

        /// <summary>
        /// Ürün günceller.
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductDto productDto)
        {
            if (productDto == null || id != productDto.Id)
            {
                _logger.LogWarning("Ürün Id uyuşmazlığı. Gönderilen Id: {Id}, DTO Id: {DtoId}", id, productDto?.Id);
                return ResponseHelper.BadRequest("Ürün Id uyuşmazlığı.");
            }

            await _productService.UpdateProductAsync(productDto);
            _logger.LogInformation("Ürün başarıyla güncellendi. Id: {ProductId}", id);

            return ResponseHelper.Ok("Ürün başarıyla güncellendi.", productDto);
        }

        /// <summary>
        /// Ürün siler.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            await _productService.DeleteProductAsync(id);
            _logger.LogInformation("Ürün başarıyla silindi. Id: {ProductId}", id);

            return ResponseHelper.NoContent();
        }
    }



}
