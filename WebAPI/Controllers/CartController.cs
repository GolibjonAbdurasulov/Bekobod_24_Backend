using Microsoft.AspNetCore.Mvc;
using Services.DTOs;
using Services.Implementations;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/cart")]
public class CartController : ControllerBase
{
    private readonly CartService _service;

    public CartController(CartService service) => _service = service;

    [HttpGet("{userId}")]
    public async Task<CartDto> GetCart(long userId)
    {
        return await _service.GetOrCreateCart(userId);
    }

    [HttpPost("{userId}/items")]
    public async Task<ActionResult<CartDto>> AddItem(long userId, [FromBody] AddToCartDto dto)
    {
        await _service.AddItem(userId, dto);
        var cart = await _service.GetOrCreateCart(userId);
        return cart;
    }

    [HttpDelete("items/{itemId}")]
    public async Task<IActionResult> RemoveItem(long itemId)
    {
        await _service.RemoveItem(itemId);
        return NoContent();
    }

    [HttpDelete("{userId}")]
    public async Task<IActionResult> ClearCart(long userId)
    {
        await _service.ClearCart(userId);
        return NoContent();
    }
}
