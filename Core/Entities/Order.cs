using System.ComponentModel.DataAnnotations.Schema;
using Core.Enums;

namespace Core.Entities;

[Table("orders")]
public class Order : ModelBase<long>
{
    [Column("user_id")]
    public long UserId { get; set; }

    [ForeignKey(nameof(UserId))]
    public User User { get; set; } = null!;

    [Column("total_price")]
    public decimal TotalPrice { get; set; }

    [Column("status")]
    public OrderStatus Status { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public List<OrderItem> Items { get; set; } = new();
}
