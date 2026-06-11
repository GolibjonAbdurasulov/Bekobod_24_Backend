using Core.Entities;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Services.Implementations;

public class CartService
{
    private readonly AppDbContext _db;

    public CartService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<Cart> GetOrCreateCart(long userId)
    {
        var cart = await _db.Carts
            .Include(x => x.Items)
            .FirstOrDefaultAsync(x => x.UserId == userId);

        if (cart == null)
        {
            cart = new Cart { UserId = userId };
            _db.Carts.Add(cart);
            await _db.SaveChangesAsync();
        }

        return cart;
    }

    public async Task AddItem(long userId, CartItem item)
    {
        var cart = await GetOrCreateCart(userId);

        item.CartId = cart.Id;
        _db.CartItems.Add(item);

        await _db.SaveChangesAsync();
    }

    public async Task RemoveItem(long itemId)
    {
        var item = await _db.CartItems.FindAsync(itemId);
        if (item == null) return;

        _db.CartItems.Remove(item);
        await _db.SaveChangesAsync();
    }
}