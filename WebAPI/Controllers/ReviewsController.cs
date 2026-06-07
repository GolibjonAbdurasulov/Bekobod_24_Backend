using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.DTOs.Review;
using Services.Interfaces;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/reviews")]
public class ReviewsController : ControllerBase
{
    private readonly IReviewService _reviewService;

    public ReviewsController(IReviewService reviewService)
    {
        _reviewService = reviewService;
    }

    [HttpPost]
    [Authorize(Roles = "Client")]
    public async Task<IActionResult> Create(CreateReviewRequest request)
    {
        try
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var result = await _reviewService.CreateAsync(request, userId);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpGet("by-store/{storeId}")]
    public async Task<IActionResult> GetByStore(Guid storeId)
    {
        var result = await _reviewService.GetByStoreAsync(storeId);
        return Ok(result);
    }

    [HttpGet("by-courier/{courierId}")]
    public async Task<IActionResult> GetByCourier(Guid courierId)
    {
        var result = await _reviewService.GetByCourierAsync(courierId);
        return Ok(result);
    }
}
