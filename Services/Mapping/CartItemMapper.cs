using Core.Entities;
using Services.DTOs;

namespace Services.Mapping;

public static class CartItemMapper
{
    public static CartItemDto ToDto(CartItem i) => new()
    {
        Id = i.Id,
        StoreId = i.StoreId,
        ProductId = i.ProductId,
        ServiceId = i.ServiceId,
        Name = i.Name,
        Price = i.Price,
        Quantity = i.Quantity,
        BookingTime = i.BookingTime
    };
}
