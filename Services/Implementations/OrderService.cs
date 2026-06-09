using Core.Entities;
using Core.Enums;
using Core.Exceptions;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Services.DTOs.Order;
using Services.Interfaces;

namespace Services.Implementations;

public class OrderService : IOrderService
{
    private readonly AppDbContext _db;

    public OrderService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<OrderResponse> CreateAsync(CreateOrderRequest request, Guid clientId)
    {
        var store = await _db.Stores.FindAsync(request.StoreId)
            ?? throw new NotFoundException("Do'kon topilmadi");

        var productIds = request.Items.Select(i => i.ProductId).ToList();
        var products = await _db.Products
            .Where(p => productIds.Contains(p.Id))
            .ToDictionaryAsync(p => p.Id);

        var items = new List<OrderItem>();
        decimal total = 0;

        foreach (var item in request.Items)
        {
            if (!products.TryGetValue(item.ProductId, out var product))
                throw new NotFoundException($"Mahsulot topilmadi: {item.ProductId}");

            var orderItem = new OrderItem
            {
                ProductId = product.Id,
                ProductName = product.Name,
                UnitPrice = product.Price,
                Quantity = item.Quantity,
                TotalPrice = product.Price * item.Quantity
            };

            items.Add(orderItem);
            total += orderItem.TotalPrice;
        }

        var order = new Order
        {
            OrderNumber = GenerateOrderNumber(),
            ClientId = clientId,
            StoreId = request.StoreId,
            DeliveryAddress = request.DeliveryAddress,
            DeliveryLatitude = request.DeliveryLatitude,
            DeliveryLongitude = request.DeliveryLongitude,
            Note = request.Note,
            TotalAmount = total,
            Status = OrderStatus.New
        };

        _db.Orders.Add(order);
        await _db.SaveChangesAsync();

        foreach (var item in items)
        {
            item.OrderId = order.Id;
        }

        _db.OrderItems.AddRange(items);
        await _db.SaveChangesAsync();

        return await GetByIdAsync(order.Id)
            ?? throw new Exception("Buyurtma yaratishda xatolik");
    }

    public async Task<OrderResponse?> GetByIdAsync(Guid id)
    {
        var order = await _db.Orders
            .Include(o => o.Client)
            .Include(o => o.Courier)
            .Include(o => o.Store)
            .FirstOrDefaultAsync(o => o.Id == id);

        if (order == null) return null;

        var items = await _db.OrderItems
            .Where(i => i.OrderId == id)
            .ToListAsync();

        return MapToResponse(order, items);
    }

    public async Task<List<OrderResponse>> GetByClientAsync(Guid clientId)
    {
        var orders = await _db.Orders
            .Include(o => o.Client)
            .Include(o => o.Courier)
            .Include(o => o.Store)
            .Where(o => o.ClientId == clientId)
            .OrderByDescending(o => o.CreatedAt)
            .ToListAsync();

        var orderIds = orders.Select(o => o.Id).ToList();
        var items = await _db.OrderItems
            .Where(i => orderIds.Contains(i.OrderId))
            .ToListAsync();

        var grouped = items.GroupBy(i => i.OrderId)
            .ToDictionary(g => g.Key, g => g.ToList());

        return orders.Select(o => MapToResponse(o,
            grouped.GetValueOrDefault(o.Id, new List<OrderItem>()))).ToList();
    }

    public async Task<OrderResponse> AssignCourierAsync(Guid orderId, Guid courierId)
    {
        var order = await _db.Orders.FindAsync(orderId)
            ?? throw new NotFoundException("Buyurtma topilmadi");

        var courier = await _db.Couriers.Include(c => c.User)
            .FirstOrDefaultAsync(c => c.UserId == courierId)
            ?? throw new NotFoundException("Kuryer topilmadi");

        if (!courier.IsAvailable)
            throw new NotAllowedException("Kuryer band");

        order.CourierId = courier.UserId;
        order.Status = OrderStatus.OutForDelivery;

        await _db.SaveChangesAsync();

        return await GetByIdAsync(orderId)
            ?? throw new Exception("Xatolik");
    }

    public async Task<OrderResponse> MarkDeliveredAsync(Guid orderId)
    {
        var order = await _db.Orders.FindAsync(orderId)
            ?? throw new NotFoundException("Buyurtma topilmadi");

        order.Status = OrderStatus.Delivered;
        order.DeliveredAt = DateTime.UtcNow;

        var courier = await _db.Couriers.FirstOrDefaultAsync(c => c.UserId == order.CourierId);
        if (courier != null)
        {
            courier.IsAvailable = true;
            courier.TotalDeliveries++;
        }

        await _db.SaveChangesAsync();

        return await GetByIdAsync(orderId)
            ?? throw new Exception("Xatolik");
    }

    public async Task<List<OrderResponse>> GetByCourierAsync(Guid courierId)
    {
        var orders = await _db.Orders
            .Include(o => o.Client)
            .Include(o => o.Courier)
            .Include(o => o.Store)
            .Where(o => o.CourierId == courierId)
            .OrderByDescending(o => o.CreatedAt)
            .ToListAsync();

        var orderIds = orders.Select(o => o.Id).ToList();
        var items = await _db.OrderItems
            .Where(i => orderIds.Contains(i.OrderId))
            .ToListAsync();

        var grouped = items.GroupBy(i => i.OrderId)
            .ToDictionary(g => g.Key, g => g.ToList());

        return orders.Select(o => MapToResponse(o,
            grouped.GetValueOrDefault(o.Id, new List<OrderItem>()))).ToList();
    }

    public async Task<List<OrderResponse>> GetAvailableForCourierAsync()
    {
        var orders = await _db.Orders
            .Include(o => o.Client)
            .Include(o => o.Courier)
            .Include(o => o.Store)
            .Where(o => o.Status == OrderStatus.New)
            .OrderByDescending(o => o.CreatedAt)
            .ToListAsync();

        var orderIds = orders.Select(o => o.Id).ToList();
        var items = await _db.OrderItems
            .Where(i => orderIds.Contains(i.OrderId))
            .ToListAsync();

        var grouped = items.GroupBy(i => i.OrderId)
            .ToDictionary(g => g.Key, g => g.ToList());

        return orders.Select(o => MapToResponse(o,
            grouped.GetValueOrDefault(o.Id, new List<OrderItem>()))).ToList();
    }

    public async Task<OrderResponse> AcceptByCourierAsync(Guid orderId, Guid courierId)
    {
        var order = await _db.Orders.FindAsync(orderId)
            ?? throw new NotFoundException("Buyurtma topilmadi");

        if (order.Status != OrderStatus.New)
            throw new NotAllowedException("Buyurtma allaqachon qabul qilingan");

        var courier = await _db.Couriers
            .FirstOrDefaultAsync(c => c.UserId == courierId)
            ?? throw new NotFoundException("Kuryer topilmadi");

        if (!courier.IsAvailable)
            throw new NotAllowedException("Siz band qilib belgilangansiz");

        order.CourierId = courier.UserId;
        order.Status = OrderStatus.OutForDelivery;
        courier.IsAvailable = false;

        await _db.SaveChangesAsync();

        return await GetByIdAsync(orderId)
            ?? throw new Exception("Xatolik");
    }

    public async Task<List<OrderResponse>> GetCourierHistoryAsync(Guid courierId)
    {
        var orders = await _db.Orders
            .Include(o => o.Client)
            .Include(o => o.Courier)
            .Include(o => o.Store)
            .Where(o => o.CourierId == courierId && o.Status == OrderStatus.Delivered)
            .OrderByDescending(o => o.DeliveredAt)
            .ToListAsync();

        var orderIds = orders.Select(o => o.Id).ToList();
        var items = await _db.OrderItems
            .Where(i => orderIds.Contains(i.OrderId))
            .ToListAsync();

        var grouped = items.GroupBy(i => i.OrderId)
            .ToDictionary(g => g.Key, g => g.ToList());

        return orders.Select(o => MapToResponse(o,
            grouped.GetValueOrDefault(o.Id, new List<OrderItem>()))).ToList();
    }

    private static OrderResponse MapToResponse(Order order, List<OrderItem> items)
    {
        return new OrderResponse
        {
            Id = order.Id,
            OrderNumber = order.OrderNumber,
            Status = order.Status.ToString(),
            TotalAmount = order.TotalAmount,
            DeliveryAddress = order.DeliveryAddress,
            Note = order.Note,
            CreatedAt = order.CreatedAt,
            DeliveredAt = order.DeliveredAt,
            ClientName = order.Client.FullName,
            ClientPhone = order.Client.Phone,
            CourierName = order.Courier?.FullName,
            CourierPhone = order.Courier?.Phone,
            StoreName = order.Store.Name,
            Items = items.Select(i => new OrderItemResponse
            {
                ProductId = i.ProductId,
                ProductName = i.ProductName,
                UnitPrice = i.UnitPrice,
                Quantity = i.Quantity,
                TotalPrice = i.TotalPrice
            }).ToList()
        };
    }

    private static string GenerateOrderNumber()
    {
        return $"B24-{DateTime.UtcNow:yyyyMMdd}-{Random.Shared.Next(1000, 9999)}";
    }
}
