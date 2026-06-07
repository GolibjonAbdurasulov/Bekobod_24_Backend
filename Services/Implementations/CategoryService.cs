using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Services.DTOs.Category;
using Services.Interfaces;

namespace Services.Implementations;

public class CategoryService : ICategoryService
{
    private readonly AppDbContext _db;

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
                Name = c.Name
            })
            .ToListAsync();
    }
}
