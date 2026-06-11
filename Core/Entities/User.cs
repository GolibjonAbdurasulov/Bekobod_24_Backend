using Core.Enums;

namespace Core.Entities;

public class User
{
    public long Id { get; set; }

    public long TelegramId { get; set; }

    public string? Username { get; set; }

    public string? FirstName { get; set; }

    public string? PhoneNumber { get; set; }

    public UserRole Role { get; set; } = UserRole.Client;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}