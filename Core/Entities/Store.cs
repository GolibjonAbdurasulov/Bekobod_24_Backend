using System.ComponentModel.DataAnnotations.Schema;
using Core.Enums;

namespace Core.Entities;

[Table("stores")]
public class Store : ModelBase<long>
{
    [Column("name")]
    public string Name { get; set; } = string.Empty;

    [Column("type")]
    public StoreType Type { get; set; }

    [Column("image_id")]
    public Guid? ImageId { get; set; }

    [ForeignKey(nameof(ImageId))]
    public FileModel? Image { get; set; }

    [Column("is_active")]
    public bool IsActive { get; set; } = true;

    public List<Product> Products { get; set; } = new();
    public List<Service> Services { get; set; } = new();
}
