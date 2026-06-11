using Core.Entities;
using Microsoft.AspNetCore.Http;

namespace Services.Interfaces;

public interface IFileService
{
    public Task<FileModel> UploadAsync(IFormFile file);

    public Task<FileModel> UpdateAsync(Guid id, IFormFile file);

    public Task<bool> DeleteAsync(Guid id);

    public Task<FileModel> GetByIdAsync(Guid id);

    public Task<Stream> GetStreamAsync(Guid id);
}