using Core.Entities;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Services.Implementations;
public class UserService : CrudService<User>
{
    public UserService(AppDbContext db) : base(db) { }

    public async Task<User?> GetByTelegramId(long telegramId)
    {
        return await _db.Users
            .FirstOrDefaultAsync(x => x.TelegramId == telegramId);
    }
}