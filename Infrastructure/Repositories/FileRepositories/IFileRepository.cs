using Core.Entities;
using Infrastructure.Repositories.Common;

namespace Infrastructure.Repositories.FileRepositories;

public interface IFileRepository : IRepositoryBase<FileModel,Guid>
{
}
