namespace Services.DTOs.Category;

public class CategoryResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public Guid? ImageId { get; set; }
    public string? ImageUrl { get; set; }
}
