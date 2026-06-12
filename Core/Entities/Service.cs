using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities;

[Table("services")]
public class Service : ModelBase<long>
{
    [Column("store_id")]
    public long StoreId { get; set; }

    [ForeignKey(nameof(StoreId))]
    public Store Store { get; set; } = null!;

    [Column("name")]
    public string Name { get; set; } = string.Empty;

    [Column("price")]
    public decimal Price { get; set; }

    [Column("description")]
    public string? Description { get; set; }

    [Column("requires_booking")]
    public bool RequiresBooking { get; set; } = true;
}
