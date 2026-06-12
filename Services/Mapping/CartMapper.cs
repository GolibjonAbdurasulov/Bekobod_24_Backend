using Core.Entities;
using Services.DTOs;

namespace Services.Mapping;

public static class CartMapper
{
    public static CartDto ToDto(Cart c) => new()
    {
        Id = c.Id,
        UserId = c.UserId,
        Items = c.Items.Select(CartItemMapper.ToDto).ToList()
    };
}
