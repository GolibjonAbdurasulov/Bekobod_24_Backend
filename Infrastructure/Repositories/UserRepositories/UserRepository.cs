using Core.Attributes;
using Core.Entities;
using Infrastructure.Repositories.Common;

namespace Infrastructure.Repositories.UserRepositories;

[Injectable]
public class UserRepository : RepositoryBase<User,long>, IUserRepository
{
    public UserRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}
