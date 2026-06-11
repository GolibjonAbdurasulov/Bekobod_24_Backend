using Core.Entities;
using WebAPI.DTOs;

namespace WebAPI.Mapping;

public static class ServiceMapper
{
    public static ServiceDto ToDto(Service service)
    {
        if (service == null) return null;

        return new ServiceDto
        {
            Id = service.Id,
            StoreId = service.StoreId,
            Name = service.Name,
            Price = service.Price,
            Description = service.Description,
            RequiresBooking = service.RequiresBooking
        };
    }

    public static List<ServiceDto> ToDtoList(List<Service> services)
    {
        return services.Select(ToDto).ToList();
    }
}