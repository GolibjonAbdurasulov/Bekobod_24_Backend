using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Services.DTOs.Product;
using Services.Interfaces;

namespace Services.Implementations;

public class ProductService : IProductService
{
    private readonly AppDbContext _db;

    public ProductService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<List<ProductResponse>> GetByStoreAsync(Guid storeId, Guid? categoryId)
    {
        var query = _db.Products
            .Where(p => p.StoreId == storeId && p.IsAvailable);

        if (categoryId.HasValue)
            query = query.Where(p => p.CategoryId == categoryId);

        return await query
            .Select(p => new ProductResponse
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                ImageUrl = p.ImageUrl,
                Unit = p.Unit,
                Attributes = p.Attributes,
                CategoryId = p.CategoryId,
                CategoryName = p.Category.Name
            })
            .ToListAsync();
    }

    public async Task<ProductResponse?> GetByIdAsync(Guid id)
    {
        return await _db.Products
            .Where(p => p.Id == id)
            .Select(p => new ProductResponse
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                ImageUrl = p.ImageUrl,
                Unit = p.Unit,
                Attributes = p.Attributes,
                CategoryId = p.CategoryId,
                CategoryName = p.Category.Name
            })
            .FirstOrDefaultAsync();
    }
}
