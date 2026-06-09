using Core.Entities;
using Microsoft.AspNetCore.Http;

namespace Services.Interfaces;

public interface IFileService
{
    Task<FileModel> UploadAsync(IFormFile file);
    Task<FileModel> UpdateAsync(Guid id, IFormFile file);
    Task DeleteAsync(Guid id);
    Task<FileModel> GetByIdAsync(Guid id);
    Task<Stream> GetStreamAsync(Guid id);
}
