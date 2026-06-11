using Core.Entities;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Services.Implementations;

public class ServiceService : CrudService<Service>
{
    public ServiceService(AppDbContext db) : base(db) { }

    public async Task<List<Service>> GetByStore(long storeId)
    {
        return await _db.Services
            .Where(x => x.StoreId == storeId)
            .ToListAsync();
    }
}