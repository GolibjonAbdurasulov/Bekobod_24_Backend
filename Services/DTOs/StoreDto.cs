using Services.Settings;

namespace Services.DTOs;

public class StoreDto
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public Guid? ImageId { get; set; }
    public string? ImageUrl => ImageId.HasValue
        ? $"{ApiSettings.BaseUrl}/File/DownloadFile/download/{ImageId}"
        : null;
    public bool IsActive { get; set; }
}

public class CreateStoreDto
{
    public string Name { get; set; } = string.Empty;
    public int Type { get; set; }
    public Guid? ImageId { get; set; }
}

public class UpdateStoreDto
{
    public string Name { get; set; } = string.Empty;
    public int Type { get; set; }
    public Guid? ImageId { get; set; }
    public bool IsActive { get; set; }
}
