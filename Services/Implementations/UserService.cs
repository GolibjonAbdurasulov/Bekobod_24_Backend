using Core.Entities;
using Core.Enums;
using Infrastructure.Repositories.UserRepositories;
using Services.DTOs;
using Services.Mapping;

namespace Services.Implementations;

public class UserService
{
    private readonly IUserRepository _repo;

    public UserService(IUserRepository repo) => _repo = repo;

    public async Task<List<UserDto>> GetAllAsync()
    {
        var users = await _repo.GetAllAsync();
        return users.Select(UserMapper.ToDto).ToList();
    }

    public async Task<UserDto?> GetByIdAsync(long id)
    {
        var user = await _repo.GetByIdAsync(id);
        return user is null ? null : UserMapper.ToDto(user);
    }

    public async Task<UserDto?> GetByTelegramId(long telegramId)
    {
        var users = await _repo.GetAllAsync();
        var user = users.FirstOrDefault(u => u.TelegramId == telegramId);
        return user is null ? null : UserMapper.ToDto(user);
    }

    public async Task<UserDto> SyncAsync(SyncUserDto dto)
    {
        var users = await _repo.GetAllAsync();
        var user = users.FirstOrDefault(u => u.TelegramId == dto.TelegramId);

        if (user is null)
        {
            user = new User
            {
                TelegramId = dto.TelegramId,
                Username = dto.Username,
                FirstName = dto.FirstName,
                Role = UserRole.Client
            };
            user = await _repo.AddAsync(user);
        }
        else
        {
            user.Username = dto.Username ?? user.Username;
            user.FirstName = dto.FirstName ?? user.FirstName;
            user = await _repo.UpdateAsync(user);
        }

        return UserMapper.ToDto(user);
    }

    public async Task<UserDto?> UpdateAsync(long id, UpdateUserDto dto)
    {
        var user = await _repo.GetByIdAsync(id);
        if (user is null) return null;

        user.FirstName = dto.FirstName ?? user.FirstName;
        user.PhoneNumber = dto.PhoneNumber ?? user.PhoneNumber;

        user = await _repo.UpdateAsync(user);
        return UserMapper.ToDto(user);
    }

    public async Task<bool> DeleteAsync(long id)
    {
        return await _repo.RemoveByIdAsync(id);
    }
}
