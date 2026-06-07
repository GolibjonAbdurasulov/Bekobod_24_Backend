using Services.DTOs.Order;

namespace Services.Interfaces;

public interface IOrderService
{
    Task<OrderResponse> CreateAsync(CreateOrderRequest request, Guid clientId);
    Task<OrderResponse?> GetByIdAsync(Guid id);
    Task<List<OrderResponse>> GetByClientAsync(Guid clientId);
    Task<OrderResponse> AssignCourierAsync(Guid orderId, Guid courierId);
    Task<OrderResponse> MarkDeliveredAsync(Guid orderId);
}
