using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.DTOs.Courier;
using Services.Interfaces;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/couriers")]
public class CouriersController : ControllerBase
{
    private readonly ICourierService _courierService;

    public CouriersController(ICourierService courierService)
    {
        _courierService = courierService;
    }

    [HttpGet("available")]
    public async Task<IActionResult> GetAvailable()
    {
        var result = await _courierService.GetAvailableAsync();
        return Ok(result);
    }

    [HttpGet("profile")]
    [Authorize(Roles = "Courier")]
    public async Task<IActionResult> GetProfile()
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var result = await _courierService.GetProfileAsync(userId);
        if (result == null)
            return NotFound(new { error = "Kuryer topilmadi" });
        return Ok(result);
    }

    [HttpPut("profile")]
    [Authorize(Roles = "Courier")]
    public async Task<IActionResult> UpdateProfile(UpdateCourierProfileRequest request)
    {
        try
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var result = await _courierService.UpdateProfileAsync(userId, request);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
}
