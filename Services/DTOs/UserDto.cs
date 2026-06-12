namespace Services.DTOs;

public class UserDto
{
    public long Id { get; set; }
    public long TelegramId { get; set; }
    public string? Username { get; set; }
    public string? FirstName { get; set; }
    public string? PhoneNumber { get; set; }
    public string Role { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}

public class SyncUserDto
{
    public long TelegramId { get; set; }
    public string? Username { get; set; }
    public string? FirstName { get; set; }
}

public class UpdateUserDto
{
    public string? PhoneNumber { get; set; }
    public string? FirstName { get; set; }
}
