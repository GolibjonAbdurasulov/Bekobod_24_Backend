using Core.Enums;

namespace Core.Entities;

public class Order
{
    public long Id { get; set; }

    public long UserId { get; set; }
    public User User { get; set; }

    public decimal TotalPrice { get; set; }

    public OrderStatus Status { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public List<OrderItem> Items { get; set; } = new();
}