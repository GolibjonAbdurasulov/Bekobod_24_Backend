using Core.Entities;
using Services.DTOs;

namespace Services.Mapping;

public static class StoreMapper
{
    public static StoreDto ToDto(Store s) => new()
    {
        Id = s.Id,
        Name = s.Name,
        Type = s.Type.ToString(),
        ImageId = s.ImageId,
        IsActive = s.IsActive
    };
}
