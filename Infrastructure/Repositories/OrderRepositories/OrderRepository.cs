using Core.Attributes;
using Core.Entities;
using Infrastructure.Repositories.Common;

namespace Infrastructure.Repositories.OrderRepositories;

[Injectable]
public class OrderRepository : RepositoryBase<Order,long>, IOrderRepository
{
    public OrderRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}
