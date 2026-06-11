using Core.Entities;
using Core.Enums;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Services.Implementations;
public class StoreService : CrudService<Store>
{
    public StoreService(AppDbContext db) : base(db) { }

    public async Task<List<Store>> GetByType(StoreType type)
    {
        return await _db.Stores
            .Where(x => x.Type == type)
            .ToListAsync();
    }
}