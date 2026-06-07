using System.Security.Cryptography;
using Core.Entities;
using Core.Enums;
using Core.Exceptions;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Services.DTOs.Auth;
using Services.Interfaces;

namespace Services.Implementations;

public class UserService : IUserService
{
    private readonly AppDbContext _db;
    private readonly ITokenService _tokenService;

    public UserService(AppDbContext db, ITokenService tokenService)
    {
        _db = db;
        _tokenService = tokenService;
    }

    public async Task<AuthResponse> RegisterAsync(RegisterRequest request)
    {
        var exists = await _db.Users.AnyAsync(u => u.Phone == request.Phone);
        if (exists)
            throw new DuplicateEntryException("Bu telefon raqam allaqachon ro'yxatdan o'tgan");

        var user = new User
        {
            FullName = request.FullName,
            Phone = request.Phone,
            PasswordHash = HashPassword(request.Password),
            Role = UserRole.Client
        };

        _db.Users.Add(user);
        await _db.SaveChangesAsync();

        return MapToResponse(user);
    }

    public async Task<AuthResponse> LoginAsync(LoginRequest request)
    {
        var user = await _db.Users.FirstOrDefaultAsync(u => u.Phone == request.Phone);
        if (user == null || !VerifyPassword(request.Password, user.PasswordHash))
            throw new NotFoundException("Telefon raqam yoki parol noto'g'ri");

        return MapToResponse(user);
    }

    private AuthResponse MapToResponse(User user)
    {
        return new AuthResponse
        {
            UserId = user.Id,
            FullName = user.FullName,
            Phone = user.Phone,
            Role = user.Role.ToString(),
            Token = _tokenService.GenerateToken(user)
        };
    }

    private static string HashPassword(string password)
    {
        var salt = RandomNumberGenerator.GetBytes(16);
        var hash = Rfc2898DeriveBytes.Pbkdf2(
            password, salt, 100000, HashAlgorithmName.SHA256, 32);
        return Convert.ToBase64String(salt) + ":" + Convert.ToBase64String(hash);
    }

    private static bool VerifyPassword(string password, string stored)
    {
        var parts = stored.Split(':');
        if (parts.Length != 2) return false;
        var salt = Convert.FromBase64String(parts[0]);
        var storedHash = Convert.FromBase64String(parts[1]);
        var hash = Rfc2898DeriveBytes.Pbkdf2(
            password, salt, 100000, HashAlgorithmName.SHA256, 32);
        return CryptographicOperations.FixedTimeEquals(hash, storedHash);
    }
}
