using Core.Entities;
using Services.DTOs;

namespace Services.Mapping;

public static class ProductMapper
{
    public static ProductDto ToDto(Product p) => new()
    {
        Id = p.Id,
        StoreId = p.StoreId,
        Name = p.Name,
        Description = p.Description,
        Price = p.Price,
        ImageId = p.ImageId,
        IsAvailable = p.IsAvailable
    };
}
