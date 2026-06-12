using Core.Enums;
using Microsoft.AspNetCore.Mvc;
using Services.DTOs;
using Services.Implementations;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/orders")]
public class OrdersController : ControllerBase
{
    private readonly OrderService _service;

    public OrdersController(OrderService service) => _service = service;

    [HttpGet]
    public async Task<List<OrderDto>> GetAll()
    {
        return await _service.GetAllOrders();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<OrderDto>> GetById(long id)
    {
        var order = await _service.GetOrderById(id);
        if (order == null) return NotFound();
        return order;
    }

    [HttpGet("user/{userId}")]
    public async Task<List<OrderDto>> GetUserOrders(long userId)
    {
        return await _service.GetUserOrders(userId);
    }

    [HttpPost("checkout/{userId}")]
    public async Task<ActionResult<OrderDto>> Checkout(long userId)
    {
        var order = await _service.CreateOrderFromCart(userId);
        return CreatedAtAction(nameof(GetById), new { id = order.Id }, order);
    }

    [HttpPatch("{id}/status")]
    public async Task<ActionResult<OrderDto>> UpdateStatus(long id, [FromBody] UpdateOrderStatusDto dto)
    {
        if (!Enum.TryParse<OrderStatus>(dto.Status, true, out var status))
            return BadRequest("Noto'g'ri status");

        var order = await _service.UpdateStatus(id, status);
        if (order == null) return NotFound();
        return order;
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(long id)
    {
        var result = await _service.DeleteOrder(id);
        if (!result) return NotFound();
        return NoContent();
    }
}
