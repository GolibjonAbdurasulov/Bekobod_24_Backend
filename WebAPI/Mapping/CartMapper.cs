using Core.Entities;
using WebAPI.DTOs;

namespace WebAPI.Mapping;

public static class CartMapper
{
    public static CartDto ToDto(Cart cart)
    {
        if (cart == null) return null;

        return new CartDto
        {
            Id = cart.Id,
            UserId = cart.UserId,
            Items = cart.Items?.Select(CartItemMapper.ToDto).ToList()
        };
    }
}