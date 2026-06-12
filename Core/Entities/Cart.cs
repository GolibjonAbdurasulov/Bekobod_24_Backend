using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities;

[Table("carts")]
public class Cart : ModelBase<long>
{
    [Column("user_id")]
    public long UserId { get; set; }

    public List<CartItem> Items { get; set; } = new();
}
