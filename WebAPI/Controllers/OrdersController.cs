using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.DTOs.Order;
using Services.Interfaces;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/orders")]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrdersController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpPost]
    [Authorize(Roles = "Client")]
    public async Task<IActionResult> Create(CreateOrderRequest request)
    {
        try
        {
            var clientId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var result = await _orderService.CreateAsync(request, clientId);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _orderService.GetByIdAsync(id);
        if (result == null)
            return NotFound();
        return Ok(result);
    }

    [HttpGet("my")]
    [Authorize(Roles = "Client")]
    public async Task<IActionResult> GetMyOrders()
    {
        var clientId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var result = await _orderService.GetByClientAsync(clientId);
        return Ok(result);
    }

    [HttpPatch("{id}/assign-courier")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> AssignCourier(Guid id, [FromBody] AssignCourierRequest request)
    {
        try
        {
            var result = await _orderService.AssignCourierAsync(id, request.CourierId);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpPatch("{id}/deliver")]
    [Authorize(Roles = "Courier")]
    public async Task<IActionResult> MarkDelivered(Guid id)
    {
        try
        {
            var result = await _orderService.MarkDeliveredAsync(id);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
}

public class AssignCourierRequest
{
    public Guid CourierId { get; set; }
}
