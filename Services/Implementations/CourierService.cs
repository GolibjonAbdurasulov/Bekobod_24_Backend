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
                IsAvailable = c.IsAvailable
            })
            .ToListAsync();
    }
}
