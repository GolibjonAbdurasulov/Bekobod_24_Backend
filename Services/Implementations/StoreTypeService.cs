using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Services.DTOs.StoreType;
using Services.Interfaces;

namespace Services.Implementations;

public class StoreTypeService : IStoreTypeService
{
    private readonly AppDbContext _db;

    public StoreTypeService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<List<StoreTypeResponse>> GetAllAsync()
    {
        return await _db.StoreTypes
            .Select(st => new StoreTypeResponse
            {
                Id = st.Id,
                Name = st.Name
            })
            .ToListAsync();
    }
}
