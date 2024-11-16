using AutoMapper;
using ECommerce.Business.Interfaces;
using ECommerce.Core.Interfaces;
using ECommerce.Entities.Entities.Concrete;
using ECommerce.Models.Dtos.ProductDtos;

namespace ECommerce.Business.Services;


public class ProductService : IProductService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    // Tüm ürünleri getiren metot
    public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
    {
        var products = await _unitOfWork.Products.GetAllAsync();
        var productDtos = _mapper.Map<IEnumerable<ProductDto>>(products);
        return productDtos;
    }

    // Belirli bir ürünü ID ile getiren metot
    public async Task<ProductDto> GetProductByIdAsync(int id)
    {
        var product = await _unitOfWork.Products.GetByIdAsync(id);
        if (product == null)
        {
            throw new KeyNotFoundException($"Ürün Id {id} ile ürün bulunamadı.");
        }

        var productDto = _mapper.Map<ProductDto>(product);
        return productDto;
    }

    // Yeni bir ürün ekleyen metot
    public async Task AddProductAsync(ProductDto productDto)
    {
        if (productDto == null)
        {
            throw new ArgumentNullException(nameof(productDto), "Ürün verisi boş olamaz.");
        }

        var category = await _unitOfWork.Categories.GetByIdAsync(productDto.CategoryId);
        if (category == null)
        {
            throw new KeyNotFoundException($"Kategori Id {productDto.CategoryId} ile kategori bulunamadı.");
        }

        var product = _mapper.Map<Product>(productDto);
        product.Category = category;

        await _unitOfWork.Products.AddAsync(product);
        await _unitOfWork.CompleteAsync();
    }

    // Var olan bir ürünü güncelleyen metot
    public async Task UpdateProductAsync(ProductDto productDto)
    {
        if (productDto == null)
        {
            throw new ArgumentNullException(nameof(productDto), "Ürün verisi boş olamaz.");
        }

        var product = await _unitOfWork.Products.GetByIdAsync(productDto.Id);
        if (product == null)
        {
            throw new KeyNotFoundException($"Ürün Id {productDto.Id} ile ürün bulunamadı.");
        }

        _mapper.Map(productDto, product);
        await _unitOfWork.Products.UpdateAsync(product);
        await _unitOfWork.CompleteAsync();
    }

    // Bir ürünü silen metot
    public async Task DeleteProductAsync(int id)
    {
        var product = await _unitOfWork.Products.GetByIdAsync(id);
        if (product == null)
        {
            throw new KeyNotFoundException($"Ürün Id {id} ile ürün bulunamadı.");
        }

        await _unitOfWork.Products.DeleteAsync(id);
        await _unitOfWork.CompleteAsync();
    }

    // Kategoriye göre ürünleri getiren metot
    public async Task<IEnumerable<ProductDto>> GetProductsByCategoryAsync(int categoryId)
    {
        var products = await _unitOfWork.Products.FindAsync(p => p.CategoryId == categoryId);
        if (products == null || !products.Any())
        {
            throw new KeyNotFoundException($"Kategori Id {categoryId} ile ürünler bulunamadı.");
        }

        return _mapper.Map<IEnumerable<ProductDto>>(products);
    }
}
