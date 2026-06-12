using Core.Entities;
using Services.DTOs;

namespace Services.Mapping;

public static class UserMapper
{
    public static UserDto ToDto(User u) => new()
    {
        Id = u.Id,
        TelegramId = u.TelegramId,
        Username = u.Username,
        FirstName = u.FirstName,
        PhoneNumber = u.PhoneNumber,
        Role = u.Role.ToString(),
        CreatedAt = u.CreatedAt
    };
}
