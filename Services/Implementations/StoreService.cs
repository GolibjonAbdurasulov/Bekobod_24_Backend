using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Services.DTOs.Store;
using Services.Interfaces;

namespace Services.Implementations;

public class StoreService : IStoreService
{
    private readonly AppDbContext _db;

    public StoreService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<List<StoreResponse>> GetByStoreTypeAsync(Guid storeTypeId)
    {
        return await _db.Stores
            .Where(s => s.StoreTypeId == storeTypeId && s.IsActive)
            .Select(s => new StoreResponse
            {
                Id = s.Id,
                Name = s.Name,
                Description = s.Description,
                Address = s.Address,
                Phone = s.Phone,
                ImageUrl = s.ImageUrl,
                StoreTypeId = s.StoreTypeId,
                StoreTypeName = s.StoreType.Name,
                Rating = _db.Reviews.Where(r => r.StoreId == s.Id)
                    .Average(r => (decimal?)r.Rating) ?? 0
            })
            .ToListAsync();
    }

    public async Task<StoreResponse?> GetByIdAsync(Guid id)
    {
        return await _db.Stores
            .Where(s => s.Id == id)
            .Select(s => new StoreResponse
            {
                Id = s.Id,
                Name = s.Name,
                Description = s.Description,
                Address = s.Address,
                Phone = s.Phone,
                ImageUrl = s.ImageUrl,
                StoreTypeId = s.StoreTypeId,
                StoreTypeName = s.StoreType.Name,
                Rating = _db.Reviews.Where(r => r.StoreId == s.Id)
                    .Average(r => (decimal?)r.Rating) ?? 0
            })
            .FirstOrDefaultAsync();
    }
}
