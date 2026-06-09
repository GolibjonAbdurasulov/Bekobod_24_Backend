namespace Core.Entities;

public class Category
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public Guid? StoreTypeId { get; set; }
    public StoreType? StoreType { get; set; }
    public Guid? ImageId { get; set; }
    public FileModel? Image { get; set; }
}
