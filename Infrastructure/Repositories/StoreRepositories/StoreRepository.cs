using Core.Attributes;
using Core.Entities;
using Infrastructure.Repositories.Common;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.StoreRepositories;

[Injectable]
public class StoreRepository : RepositoryBase<Store,long>, IStoreRepository
{
    public StoreRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}
