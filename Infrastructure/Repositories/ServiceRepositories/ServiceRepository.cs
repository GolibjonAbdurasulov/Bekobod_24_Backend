using Core.Attributes;
using Core.Entities;
using Infrastructure.Repositories.Common;

namespace Infrastructure.Repositories.ServiceRepositories;

[Injectable]
public class ServiceRepository : RepositoryBase<Service,long>, IServiceRepository
{
    public ServiceRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}
