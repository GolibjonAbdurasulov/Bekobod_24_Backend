using Core.Attributes;
using Core.Entities;
using Infrastructure.Repositories.Common;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.CartRepositories;

[Injectable]
public class CartRepository : RepositoryBase<Cart, long>, ICartRepository
{
    public CartRepository(AppDbContext dbContext) : base(dbContext) { }

    public async Task<Cart?> GetByUserId(long userId)
    {
        return await _set
            .Include(c => c.Items)
            .FirstOrDefaultAsync(c => c.UserId == userId);
    }
}
