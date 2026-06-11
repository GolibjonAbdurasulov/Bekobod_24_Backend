using Core.Entities;
using WebAPI.DTOs;

namespace WebAPI.Mapping;

public static class OrderItemMapper
{
    public static OrderItemDto ToDto(OrderItem item)
    {
        if (item == null) return null;

        return new OrderItemDto
        {
            Name = item.Name,
            Price = item.Price,
            Quantity = item.Quantity
        };
    }

    public static List<OrderItemDto> ToDtoList(List<OrderItem> items)
    {
        return items.Select(ToDto).ToList();
    }
}