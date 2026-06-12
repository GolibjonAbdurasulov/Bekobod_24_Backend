using Core.Entities;
using Core.Enums;
using Infrastructure.Repositories.StoreRepositories;
using Services.DTOs;
using Services.Mapping;

namespace Services.Implementations;

public class StoreService
{
    private readonly IStoreRepository _repo;

    public StoreService(IStoreRepository repo) => _repo = repo;

    public async Task<List<StoreDto>> GetAllAsync(StoreType? type)
    {
        var stores = await _repo.GetAllAsync();
        if (type.HasValue)
            stores = stores.Where(s => s.Type == type.Value).ToList();
        return stores.Select(StoreMapper.ToDto).ToList();
    }

    public async Task<StoreDto?> GetByIdAsync(long id)
    {
        var store = await _repo.GetByIdAsync(id);
        return store is null ? null : StoreMapper.ToDto(store);
    }

    public async Task<StoreDto> CreateAsync(CreateStoreDto dto)
    {
        var store = new Store
        {
            Name = dto.Name,
            Type = (StoreType)dto.Type,
            ImageId = dto.ImageId
        };
        store = await _repo.AddAsync(store);
        return StoreMapper.ToDto(store);
    }

    public async Task<StoreDto?> UpdateAsync(long id, UpdateStoreDto dto)
    {
        var store = await _repo.GetByIdAsync(id);
        if (store is null) return null;

        store.Name = dto.Name;
        store.Type = (StoreType)dto.Type;
        store.ImageId = dto.ImageId;
        store.IsActive = dto.IsActive;

        store = await _repo.UpdateAsync(store);
        return StoreMapper.ToDto(store);
    }

    public async Task<bool> DeleteAsync(long id)
    {
        return await _repo.RemoveByIdAsync(id);
    }
}
