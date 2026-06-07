using Services.DTOs.Product;

namespace Services.Interfaces;

public interface IProductService
{
    Task<List<ProductResponse>> GetByStoreAsync(Guid storeId, Guid? categoryId);
    Task<ProductResponse?> GetByIdAsync(Guid id);
}
