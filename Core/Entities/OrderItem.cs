namespace Core.Entities;

public class OrderItem
{
    public long Id { get; set; }

    public long OrderId { get; set; }
    public Order Order { get; set; }

    public long StoreId { get; set; }

    public long? ProductId { get; set; }

    public long? ServiceId { get; set; }

    public string Name { get; set; }

    public decimal Price { get; set; }

    public int Quantity { get; set; }

    public DateTime? BookingTime { get; set; }
}