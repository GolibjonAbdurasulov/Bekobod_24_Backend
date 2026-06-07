using Services.DTOs.Review;

namespace Services.Interfaces;

public interface IReviewService
{
    Task<ReviewResponse> CreateAsync(CreateReviewRequest request, Guid userId);
    Task<List<ReviewResponse>> GetByStoreAsync(Guid storeId);
    Task<List<ReviewResponse>> GetByCourierAsync(Guid courierId);
}
