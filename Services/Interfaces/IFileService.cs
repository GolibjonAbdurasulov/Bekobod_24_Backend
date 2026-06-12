using Core.Entities;
using Microsoft.AspNetCore.Http;

namespace Services.Interfaces;

public interface IFileService
{
    Task<FileModel> UploadFileAsync(IFormFile file);
    Task<FileModel> UpdateFileAsync(Guid id, IFormFile file);
    Task<FileModel> DeleteAsync(Guid id);
    Task<FileModel> GetByIdAsync(Guid id);
    Task<Stream> SendFileAsync(Guid id);
}
