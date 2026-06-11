using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Services.Interfaces;

namespace Services.Implementations;

public class CrudService<T> : ICrudService<T> where T : class
{
    protected readonly AppDbContext _db;

    public CrudService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<List<T>> GetAllAsync()
    {
        return await _db.Set<T>().ToListAsync();
    }

    public async Task<T?> GetByIdAsync(long id)
    {
        return await _db.Set<T>().FindAsync(id);
    }

    public async Task<T> CreateAsync(T entity)
    {
        _db.Set<T>().Add(entity);
        await _db.SaveChangesAsync();
        return entity;
    }

    public async Task<T> UpdateAsync(T entity)
    {
        _db.Set<T>().Update(entity);
        await _db.SaveChangesAsync();
        return entity;
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var entity = await _db.Set<T>().FindAsync(id);
        if (entity == null) return false;

        _db.Set<T>().Remove(entity);
        await _db.SaveChangesAsync();
        return true;
    }
}