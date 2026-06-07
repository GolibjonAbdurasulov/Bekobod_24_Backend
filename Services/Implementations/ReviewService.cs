using Core.Entities;
using Core.Exceptions;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Services.DTOs.Review;
using Services.Interfaces;

namespace Services.Implementations;

public class ReviewService : IReviewService
{
    private readonly AppDbContext _db;

    public ReviewService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<ReviewResponse> CreateAsync(CreateReviewRequest request, Guid userId)
    {
        var review = new Review
        {
            UserId = userId,
            Rating = request.Rating,
            Comment = request.Comment,
            StoreId = request.StoreId,
            CourierId = request.CourierId,
            OrderId = request.OrderId
        };

        _db.Reviews.Add(review);
        await _db.SaveChangesAsync();

        return new ReviewResponse
        {
            Id = review.Id,
            Rating = review.Rating,
            Comment = review.Comment,
            UserName = (await _db.Users.FindAsync(userId))?.FullName ?? "",
            CreatedAt = review.CreatedAt
        };
    }

    public async Task<List<ReviewResponse>> GetByStoreAsync(Guid storeId)
    {
        return await _db.Reviews
            .Include(r => r.User)
            .Where(r => r.StoreId == storeId)
            .OrderByDescending(r => r.CreatedAt)
            .Select(r => new ReviewResponse
            {
                Id = r.Id,
                Rating = r.Rating,
                Comment = r.Comment,
                UserName = r.User.FullName,
                CreatedAt = r.CreatedAt
            })
            .ToListAsync();
    }

    public async Task<List<ReviewResponse>> GetByCourierAsync(Guid courierId)
    {
        return await _db.Reviews
            .Include(r => r.User)
            .Where(r => r.CourierId == courierId)
            .OrderByDescending(r => r.CreatedAt)
            .Select(r => new ReviewResponse
            {
                Id = r.Id,
                Rating = r.Rating,
                Comment = r.Comment,
                UserName = r.User.FullName,
                CreatedAt = r.CreatedAt
            })
            .ToListAsync();
    }
}
