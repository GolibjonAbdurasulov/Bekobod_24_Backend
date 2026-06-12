using Core.Entities;
using Infrastructure.Repositories.Common;

namespace Infrastructure.Repositories.CartRepositories;

public interface ICartRepository : IRepositoryBase<Cart, long>
{
    Task<Cart?> GetByUserId(long userId);
}
