namespace Core.Entities;

public class Product
{
    public long Id { get; set; }

    public long StoreId { get; set; }
    public Store Store { get; set; }

    public string Name { get; set; }

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public string? ImageUrl { get; set; }

    public bool IsAvailable { get; set; } = true;
}