using Core.Entities;
using Infrastructure.Repositories.Common;

namespace Infrastructure.Repositories.OrderRepositories;

public interface IOrderRepository : IRepositoryBase<Order,long>
{
}
