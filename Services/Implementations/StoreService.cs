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
        var baseUrl = "http://localhost:5000";
        return await _db.Stores
            .Where(s => s.StoreTypeId == storeTypeId && s.IsActive)
            .Select(s => new StoreResponse
            {
                Id = s.Id,
                Name = s.Name,
                Description = s.Description,
                Address = s.Address,
                Phone = s.Phone,
                ImageId = s.ImageId,
                ImageUrl = s.Image != null
                    ? baseUrl + "/api/files/download/" + s.ImageId
                    : null,
                StoreTypeId = s.StoreTypeId,
                StoreTypeName = s.StoreType.Name,
                Rating = _db.Reviews.Where(r => r.StoreId == s.Id)
                    .Average(r => (decimal?)r.Rating) ?? 0
            })
            .ToListAsync();
    }

    public async Task<StoreResponse?> GetByIdAsync(Guid id)
    {
        var baseUrl = "http://localhost:5000";
        return await _db.Stores
            .Where(s => s.Id == id)
            .Select(s => new StoreResponse
            {
                Id = s.Id,
                Name = s.Name,
                Description = s.Description,
                Address = s.Address,
                Phone = s.Phone,
                ImageId = s.ImageId,
                ImageUrl = s.Image != null
                    ? baseUrl + "/api/files/download/" + s.ImageId
                    : null,
                StoreTypeId = s.StoreTypeId,
                StoreTypeName = s.StoreType.Name,
                Rating = _db.Reviews.Where(r => r.StoreId == s.Id)
                    .Average(r => (decimal?)r.Rating) ?? 0
            })
            .FirstOrDefaultAsync();
    }
}
