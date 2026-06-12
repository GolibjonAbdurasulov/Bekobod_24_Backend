using Core.Attributes;
using Core.Entities;
using Infrastructure.Repositories.Common;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.OrderRepositories;

[Injectable]
public class OrderRepository : RepositoryBase<Order,long>, IOrderRepository
{
    public OrderRepository(AppDbContext dbContext) : base(dbContext) { }

    public async Task<List<Order>> GetAllWithItemsAsync()
    {
        return await _set.Include(o => o.Items).AsNoTracking().ToListAsync();
    }

    public async Task<Order?> GetByIdWithItemsAsync(long id)
    {
        return await _set.Include(o => o.Items).FirstOrDefaultAsync(o => o.Id == id);
    }
}
