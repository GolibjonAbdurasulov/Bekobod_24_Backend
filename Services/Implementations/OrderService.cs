using Core.Entities;
using Core.Enums;
using Infrastructure.Repositories.CartRepositories;
using Infrastructure.Repositories.OrderItemRepositories;
using Infrastructure.Repositories.OrderRepositories;
using Services.DTOs;
using Services.Mapping;

namespace Services.Implementations;

public class OrderService
{
    private readonly IOrderRepository _orderRepo;
    private readonly IOrderItemRepository _itemRepo;
    private readonly ICartRepository _cartRepo;

    public OrderService(IOrderRepository orderRepo, IOrderItemRepository itemRepo, ICartRepository cartRepo)
    {
        _orderRepo = orderRepo;
        _itemRepo = itemRepo;
        _cartRepo = cartRepo;
    }

    public async Task<List<OrderDto>> GetAllOrders()
    {
        var orders = await _orderRepo.GetAllWithItemsAsync();
        return orders.Select(OrderMapper.ToDto).ToList();
    }

    public async Task<OrderDto?> GetOrderById(long id)
    {
        var order = await _orderRepo.GetByIdWithItemsAsync(id);
        return order is null ? null : OrderMapper.ToDto(order);
    }

    public async Task<List<OrderDto>> GetUserOrders(long userId)
    {
        var orders = await _orderRepo.GetAllWithItemsAsync();
        return orders
            .Where(o => o.UserId == userId)
            .Select(OrderMapper.ToDto)
            .ToList();
    }

    public async Task<OrderDto> CreateOrderFromCart(long userId)
    {
        var cart = await _cartRepo.GetByUserId(userId);
        if (cart is null || cart.Items.Count == 0)
            throw new Exception("Cart is empty");

        var order = new Order
        {
            UserId = userId,
            TotalPrice = cart.Items.Sum(i => i.Price * i.Quantity),
            Status = OrderStatus.Pending,
            CreatedAt = DateTime.UtcNow
        };

        order = await _orderRepo.AddAsync(order);

        foreach (var ci in cart.Items)
        {
            var oi = new OrderItem
            {
                OrderId = order.Id,
                StoreId = ci.StoreId,
                ProductId = ci.ProductId,
                ServiceId = ci.ServiceId,
                Name = ci.Name,
                Price = ci.Price,
                Quantity = ci.Quantity,
                BookingTime = ci.BookingTime
            };
            await _itemRepo.AddAsync(oi);
        }

        var result = await _orderRepo.GetByIdWithItemsAsync(order.Id);
        return OrderMapper.ToDto(result!);
    }

    public async Task<OrderDto?> UpdateStatus(long id, OrderStatus status)
    {
        var order = await _orderRepo.GetByIdAsync(id);
        if (order is null) return null;

        order.Status = status;
        order = await _orderRepo.UpdateAsync(order);
        return OrderMapper.ToDto(order);
    }

    public async Task<bool> DeleteOrder(long id)
    {
        return await _orderRepo.RemoveByIdAsync(id);
    }
}
