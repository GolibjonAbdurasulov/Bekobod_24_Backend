using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities;

[Table("cart_items")]
public class CartItem : ModelBase<long>
{
    [Column("cart_id")]
    public long CartId { get; set; }

    [ForeignKey(nameof(CartId))]
    public Cart Cart { get; set; } = null!;

    [Column("store_id")]
    public long StoreId { get; set; }

    [Column("product_id")]
    public long? ProductId { get; set; }

    [ForeignKey(nameof(ProductId))]
    public Product? Product { get; set; }

    [Column("service_id")]
    public long? ServiceId { get; set; }

    [ForeignKey(nameof(ServiceId))]
    public Service? Service { get; set; }

    [Column("name")]
    public string Name { get; set; } = string.Empty;

    [Column("price")]
    public decimal Price { get; set; }

    [Column("quantity")]
    public int Quantity { get; set; } = 1;

    [Column("booking_time")]
    public DateTime? BookingTime { get; set; }
}
