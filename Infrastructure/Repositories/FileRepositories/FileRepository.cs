using Core.Attributes;
using Core.Entities;
using Infrastructure.Repositories.Common;

namespace Infrastructure.Repositories.FileRepositories;

[Injectable]
public class FileRepository : RepositoryBase<FileModel,Guid>, IFileRepository
{
    public FileRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}
