using Core.Entities;
using Core.Exceptions;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Services.DTOs.Courier;
using Services.Interfaces;

namespace Services.Implementations;

public class CourierService : ICourierService
{
    private readonly AppDbContext _db;

    public CourierService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<List<CourierResponse>> GetAvailableAsync()
    {
        return await _db.Couriers
            .Include(c => c.User)
            .Where(c => c.IsAvailable)
            .Select(c => new CourierResponse
            {
                Id = c.Id,
                FullName = c.User.FullName,
                Phone = c.User.Phone,
                Rating = c.Rating,
                VehicleType = c.VehicleType,
                IsAvailable = c.IsAvailable,
                TotalDeliveries = c.TotalDeliveries
            })
            .ToListAsync();
    }

    public async Task<CourierResponse?> GetProfileAsync(Guid userId)
    {
        return await _db.Couriers
            .Include(c => c.User)
            .Where(c => c.UserId == userId)
            .Select(c => new CourierResponse
            {
                Id = c.Id,
                FullName = c.User.FullName,
                Phone = c.User.Phone,
                Rating = c.Rating,
                VehicleType = c.VehicleType,
                IsAvailable = c.IsAvailable,
                TotalDeliveries = c.TotalDeliveries
            })
            .FirstOrDefaultAsync();
    }

    public async Task<CourierResponse> UpdateProfileAsync(Guid userId, UpdateCourierProfileRequest request)
    {
        var courier = await _db.Couriers
            .Include(c => c.User)
            .FirstOrDefaultAsync(c => c.UserId == userId)
            ?? throw new NotFoundException("Kuryer topilmadi");

        var user = await _db.Users.FindAsync(userId)
            ?? throw new NotFoundException("Foydalanuvchi topilmadi");

        user.FullName = request.FullName;
        courier.VehicleType = request.VehicleType;
        courier.IsAvailable = request.IsAvailable;

        await _db.SaveChangesAsync();

        return new CourierResponse
        {
            Id = courier.Id,
            FullName = user.FullName,
            Phone = user.Phone,
            Rating = courier.Rating,
            VehicleType = courier.VehicleType,
            IsAvailable = courier.IsAvailable,
            TotalDeliveries = courier.TotalDeliveries
        };
    }
}
