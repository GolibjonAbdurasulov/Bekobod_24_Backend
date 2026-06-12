using Core.Entities;
using Infrastructure.Repositories.Common;

namespace Infrastructure.Repositories.UserRepositories;

public interface IUserRepository : IRepositoryBase<User,long>
{
}
