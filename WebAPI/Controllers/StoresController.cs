using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/stores")]
public class StoresController : ControllerBase
{
    private readonly IStoreService _storeService;

    public StoresController(IStoreService storeService)
    {
        _storeService = storeService;
    }

    [HttpGet]
    public async Task<IActionResult> GetByStoreType([FromQuery] Guid storeTypeId)
    {
        var result = await _storeService.GetByStoreTypeAsync(storeTypeId);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _storeService.GetByIdAsync(id);
        if (result == null)
            return NotFound();
        return Ok(result);
    }
}
