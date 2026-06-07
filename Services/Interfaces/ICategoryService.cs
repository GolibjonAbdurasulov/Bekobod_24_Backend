using Services.DTOs.Category;

namespace Services.Interfaces;

public interface ICategoryService
{
    Task<List<CategoryResponse>> GetByStoreTypeAsync(Guid? storeTypeId);
}
