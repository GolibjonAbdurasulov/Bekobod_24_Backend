using Services.DTOs.Courier;

namespace Services.Interfaces;

public interface ICourierService
{
    Task<List<CourierResponse>> GetAvailableAsync();
    Task<CourierResponse?> GetProfileAsync(Guid userId);
    Task<CourierResponse> UpdateProfileAsync(Guid userId, UpdateCourierProfileRequest request);
}
