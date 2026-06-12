using Core.Entities;
using Services.DTOs;

namespace Services.Mapping;

public static class ServiceMapper
{
    public static ServiceDto ToDto(Core.Entities.Service s) => new()
    {
        Id = s.Id,
        StoreId = s.StoreId,
        StoreName = s.Store?.Name ?? string.Empty,
        Name = s.Name,
        Price = s.Price,
        Description = s.Description,
        RequiresBooking = s.RequiresBooking
    };
}
