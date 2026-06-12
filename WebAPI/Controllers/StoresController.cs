using Core.Enums;
using Microsoft.AspNetCore.Mvc;
using Services.DTOs;
using Services.Implementations;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/stores")]
public class StoresController : ControllerBase
{
    private readonly StoreService _service;

    public StoresController(StoreService service) => _service = service;

    [HttpGet]
    public async Task<List<StoreDto>> GetAll([FromQuery] StoreType? type)
    {
        return await _service.GetAllAsync(type);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<StoreDto>> GetById(long id)
    {
        var store = await _service.GetByIdAsync(id);
        if (store == null) return NotFound();
        return store;
    }

    [HttpPost]
    public async Task<ActionResult<StoreDto>> Create([FromBody] CreateStoreDto dto)
    {
        var store = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = store.Id }, store);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<StoreDto>> Update(long id, [FromBody] UpdateStoreDto dto)
    {
        var store = await _service.UpdateAsync(id, dto);
        if (store == null) return NotFound();
        return store;
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(long id)
    {
        var result = await _service.DeleteAsync(id);
        if (!result) return NotFound();
        return NoContent();
    }
}
