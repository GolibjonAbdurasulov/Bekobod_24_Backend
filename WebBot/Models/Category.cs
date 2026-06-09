namespace WebBot.Models;

public class Category
{
    public Guid Id { get; set; }
    public string Name { get; set; } = "";
    public Guid? ImageId { get; set; }
    public string? ImageUrl { get; set; }
}
