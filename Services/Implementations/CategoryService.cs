using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Services.DTOs.Category;
using Services.Interfaces;

namespace Services.Implementations;

public class CategoryService : ICategoryService
{
    private readonly AppDbContext _db;
    private const string BaseUrl = "http://localhost:5000";

    public CategoryService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<List<CategoryResponse>> GetByStoreTypeAsync(Guid? storeTypeId)
    {
        var query = _db.Categories.AsQueryable();

        if (storeTypeId.HasValue)
            query = query.Where(c => c.StoreTypeId == storeTypeId);

        return await query
            .Select(c => new CategoryResponse
            {
                Id = c.Id,
                Name = c.Name,
                ImageId = c.ImageId,
                ImageUrl = c.Image != null
                    ? BaseUrl + "/api/files/download/" + c.ImageId
                    : "https://picsum.photos/seed/" + c.Id + "/400/400"
            })
            .ToListAsync();
    }
}
