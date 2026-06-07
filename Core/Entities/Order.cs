using Core.Enums;

namespace Core.Entities;

public class Order
{
    public Guid Id { get; set; }
    public string OrderNumber { get; set; } = string.Empty;
    public OrderStatus Status { get; set; } = OrderStatus.New;
    public decimal TotalAmount { get; set; }
    public string DeliveryAddress { get; set; } = string.Empty;
    public double? DeliveryLatitude { get; set; }
    public double? DeliveryLongitude { get; set; }
    public string? Note { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? DeliveredAt { get; set; }

    public Guid ClientId { get; set; }
    public User Client { get; set; } = null!;

    public Guid? CourierId { get; set; }
    public User? Courier { get; set; }

    public Guid StoreId { get; set; }
    public Store Store { get; set; } = null!;
}
