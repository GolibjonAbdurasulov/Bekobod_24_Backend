namespace WebBot.Models;

public class Store
{
    public Guid Id { get; set; }
    public string Name { get; set; } = "";
    public string? Description { get; set; }
    public string Address { get; set; } = "";
    public string? Phone { get; set; }
    public Guid? ImageId { get; set; }
    public string? ImageUrl { get; set; }
    public Guid StoreTypeId { get; set; }
    public string StoreTypeName { get; set; } = "";
    public decimal Rating { get; set; }
}
