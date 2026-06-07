namespace Services.DTOs.Order;

public class OrderResponse
{
    public Guid Id { get; set; }
    public string OrderNumber { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public decimal TotalAmount { get; set; }
    public string DeliveryAddress { get; set; } = string.Empty;
    public string? Note { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? DeliveredAt { get; set; }

    public string ClientName { get; set; } = string.Empty;
    public string ClientPhone { get; set; } = string.Empty;

    public string? CourierName { get; set; }
    public string? CourierPhone { get; set; }

    public string StoreName { get; set; } = string.Empty;

    public List<OrderItemResponse> Items { get; set; } = new();
}
