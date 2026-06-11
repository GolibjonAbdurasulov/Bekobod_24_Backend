using Core.Entities;
using WebAPI.DTOs;

namespace WebAPI.Mapping;

public class ProductMapper
{
    public static ProductDto ToDto(Product p)
    {
        return new ProductDto
        {
            Id = p.Id,
            StoreId = p.StoreId,
            Name = p.Name,
            Price = p.Price,
            ImageUrl = p.ImageUrl
        };
    }
}