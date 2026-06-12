using System.ComponentModel.DataAnnotations.Schema;
using Core.Enums;

namespace Core.Entities;

[Table("users")]
public class User : ModelBase<long>
{
    [Column("telegram_id")]
    public long TelegramId { get; set; }

    [Column("username")]
    public string? Username { get; set; }

    [Column("first_name")]
    public string? FirstName { get; set; }

    [Column("phone_number")]
    public string? PhoneNumber { get; set; }

    [Column("role")]
    public UserRole Role { get; set; } = UserRole.Client;

    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
