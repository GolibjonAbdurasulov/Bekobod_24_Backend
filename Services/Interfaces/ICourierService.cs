using Services.DTOs.Courier;

namespace Services.Interfaces;

public interface ICourierService
{
    Task<List<CourierResponse>> GetAvailableAsync();
}
