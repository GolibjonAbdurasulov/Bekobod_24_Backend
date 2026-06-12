using Services.Settings;

namespace Services.DTOs;

public class ProductDto
{
    public long Id { get; set; }
    public long StoreId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public Guid? ImageId { get; set; }
    public string? ImageUrl => ImageId.HasValue
        ? $"{ApiSettings.BaseUrl}/File/DownloadFile/download/{ImageId}"
        : null;
    public bool IsAvailable { get; set; }
}

public class CreateProductDto
{
    public long StoreId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public Guid? ImageId { get; set; }
}

public class UpdateProductDto
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public Guid? ImageId { get; set; }
    public bool IsAvailable { get; set; }
}
