namespace Core.Entities;

public class FileModel
{
    public Guid Id { get; set; }

    public string FileName { get; set; } = string.Empty;

    public string ContentType { get; set; } = string.Empty;

    public string Path { get; set; } = string.Empty;

    public long Size { get; set; } // bytes

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public bool IsPublic { get; set; } = true;

    // optional ownership (MUHIM)
    public long? UserId { get; set; }

    // optional relation (marketplace uchun kuchli)
    public string? EntityType { get; set; } 
    // Product, Store, Service, etc

    public long? EntityId { get; set; }
}