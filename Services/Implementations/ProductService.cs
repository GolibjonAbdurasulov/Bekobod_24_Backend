using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Services.DTOs.Product;
using Services.Interfaces;

namespace Services.Implementations;

public class ProductService : IProductService
{
    private readonly AppDbContext _db;
    private const string BaseUrl = "http://localhost:5000";

    public ProductService(AppDbContext db)
    {
        _db = db;
    }

    private static string ImageUrlOrPlaceholder(Guid? imageId, Guid seed)
    {
        return imageId.HasValue
            ? BaseUrl + "/api/files/download/" + imageId
            : "https://picsum.photos/seed/" + seed + "/400/400";
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
                ImageId = p.ImageId,
                ImageUrl = p.Image != null
                    ? BaseUrl + "/api/files/download/" + p.ImageId
                    : "https://picsum.photos/seed/" + p.Id + "/400/400",
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
                ImageId = p.ImageId,
                ImageUrl = p.Image != null
                    ? BaseUrl + "/api/files/download/" + p.ImageId
                    : "https://picsum.photos/seed/" + p.Id + "/400/400",
                Unit = p.Unit,
                Attributes = p.Attributes,
                CategoryId = p.CategoryId,
                CategoryName = p.Category.Name
            })
            .FirstOrDefaultAsync();
    }
}
