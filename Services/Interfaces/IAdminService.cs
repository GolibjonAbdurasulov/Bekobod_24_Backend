using Services.DTOs.Admin;
using Services.DTOs.StoreType;
using Services.DTOs.Store;
using Services.DTOs.Category;
using Services.DTOs.Product;
using Services.DTOs.Courier;
using Services.DTOs.Review;

namespace Services.Interfaces;

public interface IAdminService
{
    // Users
    Task<List<UserAdminResponse>> GetUsersAsync();
    Task<UserAdminResponse?> GetUserByIdAsync(Guid id);
    Task<UserAdminResponse> UpdateUserAsync(Guid id, UpdateUserRequest request);

    // StoreTypes
    Task<StoreTypeDetailResponse> CreateStoreTypeAsync(CreateStoreTypeRequest request);
    Task<List<StoreTypeDetailResponse>> GetAllStoreTypesAsync();
    Task<StoreTypeDetailResponse?> GetStoreTypeByIdAsync(Guid id);
    Task<StoreTypeDetailResponse> UpdateStoreTypeAsync(Guid id, UpdateStoreTypeRequest request);
    Task DeleteStoreTypeAsync(Guid id);

    // Stores
    Task<StoreAdminResponse> CreateStoreAsync(CreateStoreRequest request);
    Task<List<StoreAdminResponse>> GetAllStoresAsync();
    Task<StoreAdminResponse?> GetStoreByIdAsync(Guid id);
    Task<StoreAdminResponse> UpdateStoreAsync(Guid id, UpdateStoreRequest request);
    Task DeleteStoreAsync(Guid id);

    // Categories
    Task<CategoryDetailResponse> CreateCategoryAsync(CreateCategoryRequest request);
    Task<List<CategoryDetailResponse>> GetAllCategoriesAsync();
    Task<CategoryDetailResponse?> GetCategoryByIdAsync(Guid id);
    Task<CategoryDetailResponse> UpdateCategoryAsync(Guid id, UpdateCategoryRequest request);
    Task DeleteCategoryAsync(Guid id);

    // Products
    Task<ProductAdminResponse> CreateProductAsync(CreateProductRequest request);
    Task<List<ProductAdminResponse>> GetAllProductsAsync();
    Task<ProductAdminResponse?> GetProductByIdAsync(Guid id);
    Task<ProductAdminResponse> UpdateProductAsync(Guid id, UpdateProductRequest request);
    Task DeleteProductAsync(Guid id);

    // Couriers
    Task<CourierAdminResponse> CreateCourierAsync(CreateCourierRequest request);
    Task<List<CourierAdminResponse>> GetAllCouriersAsync();
    Task<CourierAdminResponse?> GetCourierByIdAsync(Guid id);
    Task<CourierAdminResponse> UpdateCourierAsync(Guid id, UpdateCourierRequest request);
    Task DeleteCourierAsync(Guid id);

    // Orders
    Task<List<OrderAdminResponse>> GetAllOrdersAsync();
    Task<OrderAdminResponse?> GetOrderByIdAsync(Guid id);
    Task<OrderAdminResponse> UpdateOrderStatusAsync(Guid id, UpdateOrderStatusRequest request);

    // Reviews
    Task<List<ReviewAdminResponse>> GetAllReviewsAsync();
    Task DeleteReviewAsync(Guid id);
}
