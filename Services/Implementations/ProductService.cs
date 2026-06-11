using Core.Entities;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Services.Implementations;
public class ProductService : CrudService<Product>
{
    public ProductService(AppDbContext db) : base(db) { }

    public async Task<List<Product>> GetByStore(long storeId)
    {
        return await _db.Products
            .Where(x => x.StoreId == storeId)
            .ToListAsync();
    }
}