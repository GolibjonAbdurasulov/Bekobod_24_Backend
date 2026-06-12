using Core.Entities;
using Infrastructure.Repositories.Common;

namespace Infrastructure.Repositories.ServiceRepositories;

public interface IServiceRepository : IRepositoryBase<Service,long>
{
}
