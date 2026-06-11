using Core.Enums;

namespace Core.Entities;

public class Store
{
    public long Id { get; set; }

    public string Name { get; set; }

    public StoreType Type { get; set; } 
    // Market, Restaurant, Pharmacy, Electronics, Service

    public string? ImageUrl { get; set; }

    public bool IsActive { get; set; } = true;

    public List<Product> Products { get; set; } = new();
}
