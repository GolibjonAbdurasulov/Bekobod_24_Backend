using Core.Entities;
using WebAPI.DTOs;

namespace WebAPI.Mapping;

public static class CartItemMapper
{
    public static CartItemDto ToDto(CartItem item)
    {
        if (item == null) return null;

        return new CartItemDto
        {
            StoreId = item.StoreId,
            ProductId = item.ProductId,
            ServiceId = item.ServiceId,
            Name = item.Name,
            Price = item.Price,
            Quantity = item.Quantity,
            BookingTime = item.BookingTime
        };
    }

    public static List<CartItemDto> ToDtoList(List<CartItem> items)
    {
        return items.Select(ToDto).ToList();
    }
}