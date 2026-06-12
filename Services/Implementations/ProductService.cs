using Core.Entities;
using Infrastructure.Repositories.ProductRepositories;
using Services.DTOs;
using Services.Mapping;

namespace Services.Implementations;

public class ProductService
{
    private readonly IProductRepository _repo;

    public ProductService(IProductRepository repo) => _repo = repo;

    public async Task<List<ProductDto>> GetAllAsync(long? storeId)
    {
        var products = await _repo.GetAllAsync();
        if (storeId.HasValue)
            products = products.Where(p => p.StoreId == storeId.Value).ToList();
        return products.Select(ProductMapper.ToDto).ToList();
    }

    public async Task<ProductDto?> GetByIdAsync(long id)
    {
        var product = await _repo.GetByIdAsync(id);
        return product is null ? null : ProductMapper.ToDto(product);
    }

    public async Task<ProductDto> CreateAsync(CreateProductDto dto)
    {
        var product = new Product
        {
            StoreId = dto.StoreId,
            Name = dto.Name,
            Description = dto.Description,
            Price = dto.Price,
            ImageId = dto.ImageId
        };
        product = await _repo.AddAsync(product);
        return ProductMapper.ToDto(product);
    }

    public async Task<ProductDto?> UpdateAsync(long id, UpdateProductDto dto)
    {
        var product = await _repo.GetByIdAsync(id);
        if (product is null) return null;

        product.Name = dto.Name;
        product.Description = dto.Description;
        product.Price = dto.Price;
        product.ImageId = dto.ImageId;
        product.IsAvailable = dto.IsAvailable;

        product = await _repo.UpdateAsync(product);
        return ProductMapper.ToDto(product);
    }

    public async Task<bool> DeleteAsync(long id)
    {
        return await _repo.RemoveByIdAsync(id);
    }
}
