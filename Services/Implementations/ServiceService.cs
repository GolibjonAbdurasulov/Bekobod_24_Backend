using Infrastructure.Repositories.ServiceRepositories;
using Services.DTOs;
using Services.Mapping;
using ServiceEntity = Core.Entities.Service;

namespace Services.Implementations;

public class ServiceService
{
    private readonly IServiceRepository _repo;

    public ServiceService(IServiceRepository repo) => _repo = repo;

    public async Task<List<ServiceDto>> GetAllAsync(long? storeId)
    {
        var services = await _repo.GetAllAsync();
        if (storeId.HasValue)
            services = services.Where(s => s.StoreId == storeId.Value).ToList();
        return services.Select(ServiceMapper.ToDto).ToList();
    }

    public async Task<ServiceDto?> GetByIdAsync(long id)
    {
        var service = await _repo.GetByIdAsync(id);
        return service is null ? null : ServiceMapper.ToDto(service);
    }

    public async Task<ServiceDto> CreateAsync(CreateServiceDto dto)
    {
        var service = new ServiceEntity
        {
            StoreId = dto.StoreId,
            Name = dto.Name,
            Price = dto.Price,
            Description = dto.Description,
            RequiresBooking = dto.RequiresBooking
        };
        service = await _repo.AddAsync(service);
        return ServiceMapper.ToDto(service);
    }

    public async Task<ServiceDto?> UpdateAsync(long id, UpdateServiceDto dto)
    {
        var service = await _repo.GetByIdAsync(id);
        if (service is null) return null;

        service.Name = dto.Name;
        service.Price = dto.Price;
        service.Description = dto.Description;
        service.RequiresBooking = dto.RequiresBooking;

        service = await _repo.UpdateAsync(service);
        return ServiceMapper.ToDto(service);
    }

    public async Task<bool> DeleteAsync(long id)
    {
        return await _repo.RemoveByIdAsync(id);
    }
}
