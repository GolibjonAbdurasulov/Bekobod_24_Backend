using Services.DTOs.StoreType;

namespace Services.Interfaces;

public interface IStoreTypeService
{
    Task<List<StoreTypeResponse>> GetAllAsync();
}
