namespace WebBot.Models;

public class CreateOrderRequest
{
    public Guid StoreId { get; set; }
    public string DeliveryAddress { get; set; } = "";
    public double? DeliveryLatitude { get; set; }
    public double? DeliveryLongitude { get; set; }
    public string? Note { get; set; }
    public List<OrderItemRequest> Items { get; set; } = new();
}

public class OrderItemRequest
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}
