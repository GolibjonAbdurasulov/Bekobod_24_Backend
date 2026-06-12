using Core.Entities;
using Infrastructure.Repositories.Common;

namespace Infrastructure.Repositories.CartItemRepositories;

public interface ICartItemRepository : IRepositoryBase<CartItem,long>
{
}
