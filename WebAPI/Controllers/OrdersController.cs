
using Microsoft.AspNetCore.Mvc;
using Services.Implementations;
using WebAPI.DTOs;
using WebAPI.Mapping;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/orders")]
public class OrderController : ControllerBase
{
    private readonly OrderService _service;

    public OrderController(OrderService service)
    {
        _service = service;
    }

    [HttpPost("checkout/{userId}")]
    public async Task<OrderDto> Checkout(long userId)
    {
        var order = await _service.CreateOrderFromCart(userId);
        return OrderMapper.ToDto(order);
    }

    [HttpGet("user/{userId}")]
    public async Task<List<OrderDto>> GetUserOrders(long userId)
    {
        var orders = await _service.GetUserOrders(userId);
        return orders.Select(OrderMapper.ToDto).ToList();
    }
}