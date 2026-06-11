using Microsoft.AspNetCore.Mvc;
using Services.Implementations;
using Core.Entities;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/cart")]
public class CartController : ControllerBase
{
    private readonly CartService _cartService;

    public CartController(CartService cartService)
    {
        _cartService = cartService;
    }

    // GET CART
    [HttpGet]
    public async Task<IActionResult> GetCart([FromHeader(Name = "x-telegram-id")] long userId)
    {
        var cart = await _cartService.GetOrCreateCart(userId);
        return Ok(cart);
    }

    // ADD ITEM
    [HttpPost("add")]
    public async Task<IActionResult> AddItem(
        [FromHeader(Name = "x-telegram-id")] long userId,
        [FromBody] CartItem item)
    {
        await _cartService.AddItem(userId, item);
        return Ok(new { message = "Added to cart" });
    }

    // REMOVE ITEM
    [HttpDelete("item/{itemId}")]
    public async Task<IActionResult> RemoveItem(long itemId)
    {
        await _cartService.RemoveItem(itemId);
        return Ok(new { message = "Removed from cart" });
    }
}