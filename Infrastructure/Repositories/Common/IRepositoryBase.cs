using Core;

namespace Infrastructure.Repositories.Common;

public interface IRepositoryBase<T, TId> where T : ModelBase<TId>
{
    Task<List<T>> GetAllAsync();
    Task<T?> GetByIdAsync(TId id);
    Task<T> AddAsync(T entity);
    Task<T> UpdateAsync(T entity);
    Task<bool> RemoveAsync(T entity);
    Task<bool> RemoveByIdAsync(TId id);
}
