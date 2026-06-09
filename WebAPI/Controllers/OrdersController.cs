using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Services.DTOs.Order;
using Services.Interfaces;
using WebAPI.Hubs;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/orders")]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _orderService;
    private readonly IHubContext<OrderHub> _hub;

    public OrdersController(IOrderService orderService, IHubContext<OrderHub> hub)
    {
        _orderService = orderService;
        _hub = hub;
    }

    [HttpPost]
    [Authorize(Roles = "Client")]
    public async Task<IActionResult> Create(CreateOrderRequest request)
    {
        try
        {
            var clientId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var result = await _orderService.CreateAsync(request, clientId);
            await _hub.Clients.Group("Couriers").SendAsync("NewOrder", result);
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

    [HttpGet("courier/my")]
    [Authorize(Roles = "Courier")]
    public async Task<IActionResult> GetMyCourierOrders()
    {
        var courierId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var result = await _orderService.GetByCourierAsync(courierId);
        return Ok(result);
    }

    [HttpGet("courier/available")]
    [Authorize(Roles = "Courier")]
    public async Task<IActionResult> GetAvailableOrders()
    {
        var result = await _orderService.GetAvailableForCourierAsync();
        return Ok(result);
    }

    [HttpPatch("{id}/accept")]
    [Authorize(Roles = "Courier")]
    public async Task<IActionResult> AcceptOrder(Guid id)
    {
        try
        {
            var courierId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var result = await _orderService.AcceptByCourierAsync(id, courierId);
            await _hub.Clients.Group("Couriers").SendAsync("OrderAccepted", result);
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

    [HttpGet("courier/history")]
    [Authorize(Roles = "Courier")]
    public async Task<IActionResult> GetCourierHistory()
    {
        var courierId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var result = await _orderService.GetCourierHistoryAsync(courierId);
        return Ok(result);
    }
}

public class AssignCourierRequest
{
    public Guid CourierId { get; set; }
}
