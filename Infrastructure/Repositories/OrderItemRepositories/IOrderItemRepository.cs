using Core.Entities;
using Infrastructure.Repositories.Common;

namespace Infrastructure.Repositories.OrderItemRepositories;

public interface IOrderItemRepository : IRepositoryBase<OrderItem,long>
{
}
