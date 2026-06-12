using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities;

[Table("products")]
public class Product : ModelBase<long>
{
    [Column("store_id")]
    public long StoreId { get; set; }

    [ForeignKey(nameof(StoreId))]
    public Store Store { get; set; } = null!;

    [Column("name")]
    public string Name { get; set; } = string.Empty;

    [Column("description")]
    public string? Description { get; set; }

    [Column("price")]
    public decimal Price { get; set; }

    [Column("image_id")]
    public Guid? ImageId { get; set; }

    [ForeignKey(nameof(ImageId))]
    public FileModel? Image { get; set; }

    [Column("is_available")]
    public bool IsAvailable { get; set; } = true;
}
