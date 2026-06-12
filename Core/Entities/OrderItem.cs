using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities;

[Table("order_items")]
public class OrderItem : ModelBase<long>
{
    [Column("order_id")]
    public long OrderId { get; set; }

    [ForeignKey(nameof(OrderId))]
    public Order Order { get; set; } = null!;

    [Column("store_id")]
    public long StoreId { get; set; }

    [Column("product_id")]
    public long? ProductId { get; set; }

    [Column("service_id")]
    public long? ServiceId { get; set; }

    [Column("name")]
    public string Name { get; set; } = string.Empty;

    [Column("price")]
    public decimal Price { get; set; }

    [Column("quantity")]
    public int Quantity { get; set; }

    [Column("booking_time")]
    public DateTime? BookingTime { get; set; }
}
