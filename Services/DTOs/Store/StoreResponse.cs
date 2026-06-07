namespace Services.DTOs.Store;

public class StoreResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string Address { get; set; } = string.Empty;
    public string? Phone { get; set; }
    public string? ImageUrl { get; set; }
    public Guid StoreTypeId { get; set; }
    public string StoreTypeName { get; set; } = string.Empty;
    public decimal Rating { get; set; }
}
