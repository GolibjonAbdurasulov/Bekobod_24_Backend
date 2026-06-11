namespace WebAPI.DTOs;

public class CartItemDto
{
    public long StoreId { get; set; }
    public long? ProductId { get; set; }
    public long? ServiceId { get; set; }

    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public DateTime? BookingTime { get; set; }
}