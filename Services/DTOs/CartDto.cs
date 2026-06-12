namespace Services.DTOs;

public class CartDto
{
    public long Id { get; set; }
    public long UserId { get; set; }
    public List<CartItemDto> Items { get; set; } = new();
    public decimal TotalPrice => Items.Sum(x => x.Price * x.Quantity);
}

public class CartItemDto
{
    public long Id { get; set; }
    public long StoreId { get; set; }
    public long? ProductId { get; set; }
    public long? ServiceId { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public DateTime? BookingTime { get; set; }
}

public class AddToCartDto
{
    public long StoreId { get; set; }
    public long? ProductId { get; set; }
    public long? ServiceId { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Quantity { get; set; } = 1;
    public DateTime? BookingTime { get; set; }
}
