using Core.Entities;
using Infrastructure.Repositories.Common;

namespace Infrastructure.Repositories.OrderRepositories;

public interface IOrderRepository : IRepositoryBase<Order,long>
{
    Task<List<Order>> GetAllWithItemsAsync();
    Task<Order?> GetByIdWithItemsAsync(long id);
}
