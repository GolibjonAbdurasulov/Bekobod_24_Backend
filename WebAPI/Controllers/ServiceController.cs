using Microsoft.AspNetCore.Mvc;
using Services.DTOs;
using Services.Implementations;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/services")]
public class ServiceController : ControllerBase
{
    private readonly ServiceService _service;

    public ServiceController(ServiceService service) => _service = service;

    [HttpGet]
    public async Task<List<ServiceDto>> GetAll([FromQuery] long? storeId)
    {
        return await _service.GetAllAsync(storeId);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ServiceDto>> GetById(long id)
    {
        var service = await _service.GetByIdAsync(id);
        if (service == null) return NotFound();
        return service;
    }

    [HttpPost]
    public async Task<ActionResult<ServiceDto>> Create([FromBody] CreateServiceDto dto)
    {
        var service = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = service.Id }, service);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ServiceDto>> Update(long id, [FromBody] UpdateServiceDto dto)
    {
        var service = await _service.UpdateAsync(id, dto);
        if (service == null) return NotFound();
        return service;
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(long id)
    {
        var result = await _service.DeleteAsync(id);
        if (!result) return NotFound();
        return NoContent();
    }
}
