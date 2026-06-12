using Microsoft.AspNetCore.Mvc;
using Services.DTOs;
using Services.Implementations;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    private readonly UserService _service;

    public UserController(UserService service) => _service = service;

    [HttpGet]
    public async Task<List<UserDto>> GetAll()
    {
        return await _service.GetAllAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserDto>> GetById(long id)
    {
        var user = await _service.GetByIdAsync(id);
        if (user == null) return NotFound();
        return user;
    }

    [HttpGet("telegram/{telegramId}")]
    public async Task<ActionResult<UserDto>> GetByTelegram(long telegramId)
    {
        var user = await _service.GetByTelegramId(telegramId);
        if (user == null) return NotFound();
        return user;
    }

    [HttpPost("sync")]
    public async Task<ActionResult<UserDto>> Sync([FromBody] SyncUserDto dto)
    {
        var user = await _service.SyncAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<UserDto>> Update(long id, [FromBody] UpdateUserDto dto)
    {
        var user = await _service.UpdateAsync(id, dto);
        if (user == null) return NotFound();
        return user;
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(long id)
    {
        var result = await _service.DeleteAsync(id);
        if (!result) return NotFound();
        return NoContent();
    }
}
