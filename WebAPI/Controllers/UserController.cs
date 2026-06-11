using Core.Entities;
using Core.Enums;
using Microsoft.AspNetCore.Mvc;
using Services.Implementations;
using WebAPI.DTOs;
using WebAPI.Mapping;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    private readonly UserService _service;

    public UserController(UserService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<List<UserDto>> GetAll()
    {
        var users = await _service.GetAllAsync();
        return users.Select(UserMapper.ToDto).ToList();
    }

    [HttpGet("telegram/{telegramId}")]
    public async Task<UserDto?> GetByTelegram(long telegramId)
    {
        var user = await _service.GetByTelegramId(telegramId);
        return UserMapper.ToDto(user);
    }

    [HttpPost("sync")]
    public async Task<UserDto> Sync(UserDto dto)
    {
        var user = await _service.GetByTelegramId(dto.TelegramId);

        if (user == null)
        {
            user = new User
            {
                TelegramId = dto.TelegramId,
                Username = dto.Username,
                FirstName = dto.FirstName,
                Role = UserRole.Client
            };

            await _service.CreateAsync(user);
        }

        return UserMapper.ToDto(user);
    }
}