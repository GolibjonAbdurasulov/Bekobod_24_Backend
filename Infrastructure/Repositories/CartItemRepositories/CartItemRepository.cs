using Core.Attributes;
using Core.Entities;
using Infrastructure.Repositories.Common;

namespace Infrastructure.Repositories.CartItemRepositories;

[Injectable]
public class CartItemRepository : RepositoryBase<CartItem,long>, ICartItemRepository
{
    public CartItemRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}
