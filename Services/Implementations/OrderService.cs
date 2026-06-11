using Core.Entities;
using Core.Enums;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Services.Implementations;
public class OrderService
{
    private readonly AppDbContext _db;

    public OrderService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<Order> CreateOrderFromCart(long userId)
    {
        var cart = await _db.Carts
            .Include(x => x.Items)
            .FirstAsync(x => x.UserId == userId);

        var order = new Order
        {
            UserId = userId,
            CreatedAt = DateTime.UtcNow,
            Status = OrderStatus.Pending,
            Items = cart.Items.Select(x => new OrderItem
            {
                StoreId = x.StoreId,
                ProductId = x.ProductId,
                ServiceId = x.ServiceId,
                Name = x.Name,
                Price = x.Price,
                Quantity = x.Quantity,
                BookingTime = x.BookingTime
            }).ToList()
        };

        order.TotalPrice = order.Items.Sum(x => x.Price * x.Quantity);

        _db.Orders.Add(order);

        _db.CartItems.RemoveRange(cart.Items);

        await _db.SaveChangesAsync();

        return order;
    }

    public async Task<List<Order>> GetUserOrders(long userId)
    {
        return await _db.Orders
            .Include(x => x.Items)
            .Where(x => x.UserId == userId)
            .ToListAsync();
    }
}