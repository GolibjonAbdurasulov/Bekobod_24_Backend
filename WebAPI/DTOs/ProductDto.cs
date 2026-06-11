namespace WebAPI.DTOs;

public class ProductDto
{
    public long Id { get; set; }
    public long StoreId { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string? ImageUrl { get; set; }
}