using Core.Entities;
using WebAPI.DTOs;

namespace WebAPI.Mapping;
public static class UserMapper
{
    public static UserDto ToDto(User user)
    {
        if (user == null) return null;

        return new UserDto
        {
            Id = user.Id,
            TelegramId = user.TelegramId,
            Username = user.Username,
            FirstName = user.FirstName,
            Role = user.Role.ToString()
        };
    }

    public static List<UserDto> ToDtoList(List<User> users)
    {
        return users.Select(ToDto).ToList();
    }
}