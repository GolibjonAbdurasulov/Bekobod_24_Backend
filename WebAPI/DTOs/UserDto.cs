namespace WebAPI.DTOs;

public class UserDto
{
    public long Id { get; set; }
    public long TelegramId { get; set; }
    public string? Username { get; set; }
    public string? FirstName { get; set; }
    public string Role { get; set; }
}