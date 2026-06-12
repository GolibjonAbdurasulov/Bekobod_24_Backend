using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities;

[Table("files")]
public class FileModel : ModelBase<Guid>
{
    [Column("file_name")]
    public string FileName { get; set; } = string.Empty;

    [Column("content_type")]
    public string ContentType { get; set; } = string.Empty;

    [Column("path")]
    public string Path { get; set; } = string.Empty;

    [Column("size")]
    public long Size { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Column("is_public")]
    public bool IsPublic { get; set; } = true;

    [Column("user_id")]
    public long? UserId { get; set; }

    [Column("entity_type")]
    public string? EntityType { get; set; }

    [Column("entity_id")]
    public long? EntityId { get; set; }
}
