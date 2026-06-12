using Core;
using Core.Attributes;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Common;

[Injectable]
public class RepositoryBase<T, TId> : IRepositoryBase<T, TId> where T : ModelBase<TId>
{
    protected readonly AppDbContext _db;
    protected readonly DbSet<T> _set;

    public RepositoryBase(AppDbContext db)
    {
        _db = db;
        _set = db.Set<T>();
    }

    public async Task<List<T>> GetAllAsync()
    {
        return await _set.AsNoTracking().ToListAsync();
    }

    public async Task<T?> GetByIdAsync(TId id)
    {
        return await _set.FindAsync(id);
    }

    public async Task<T> AddAsync(T entity)
    {
        _set.Add(entity);
        await _db.SaveChangesAsync();
        return entity;
    }

    public async Task<T> UpdateAsync(T entity)
    {
        _set.Update(entity);
        await _db.SaveChangesAsync();
        return entity;
    }

    public async Task<bool> RemoveAsync(T entity)
    {
        _set.Remove(entity);
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> RemoveByIdAsync(TId id)
    {
        var entity = await _set.FindAsync(id);
        if (entity == null) return false;
        _set.Remove(entity);
        await _db.SaveChangesAsync();
        return true;
    }
}
