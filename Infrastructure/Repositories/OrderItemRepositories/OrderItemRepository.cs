using Core.Attributes;
using Core.Entities;
using Infrastructure.Repositories.Common;

namespace Infrastructure.Repositories.OrderItemRepositories;

[Injectable]
public class OrderItemRepository : RepositoryBase<OrderItem,long>, IOrderItemRepository
{
    public OrderItemRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}
