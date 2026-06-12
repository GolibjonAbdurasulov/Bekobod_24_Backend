using Core.Entities;
using Services.DTOs;

namespace Services.Mapping;

public static class OrderMapper
{
    public static OrderDto ToDto(Order o) => new()
    {
        Id = o.Id,
        UserId = o.UserId,
        TotalPrice = o.TotalPrice,
        Status = o.Status.ToString(),
        CreatedAt = o.CreatedAt,
        Items = o.Items.Select(OrderItemMapper.ToDto).ToList()
    };
}
