namespace Core.Entities;

public class CartItem
{
    public long Id { get; set; }

    public long CartId { get; set; }
    public Cart Cart { get; set; }

    public long StoreId { get; set; }

    public long? ProductId { get; set; }
    public Product? Product { get; set; }

    public long? ServiceId { get; set; }
    public Service? Service { get; set; }

    public string Name { get; set; }

    public decimal Price { get; set; }

    public int Quantity { get; set; } = 1;

    // faqat service uchun
    public DateTime? BookingTime { get; set; }
}