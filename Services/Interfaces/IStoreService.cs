using Services.DTOs.Store;

namespace Services.Interfaces;

public interface IStoreService
{
    Task<List<StoreResponse>> GetByStoreTypeAsync(Guid storeTypeId);
    Task<StoreResponse?> GetByIdAsync(Guid id);
}
