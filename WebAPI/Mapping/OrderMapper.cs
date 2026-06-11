using Core.Entities;
using WebAPI.DTOs;

namespace WebAPI.Mapping;

public static class OrderMapper
{
    public static OrderDto ToDto(Order o)
    {
        if (o == null) return null;

        return new OrderDto
        {
            Id = o.Id,
            TotalPrice = o.TotalPrice,
            Status = o.Status.ToString(),
            Items = o.Items?.Select(OrderItemMapper.ToDto).ToList()
        };
    }

    public static List<OrderDto> ToDtoList(List<Order> orders)
    {
        return orders.Select(ToDto).ToList();
    }
}