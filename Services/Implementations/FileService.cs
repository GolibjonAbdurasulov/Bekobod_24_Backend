using Core.Entities;
using Infrastructure.Repositories.FileRepositories;
using Microsoft.AspNetCore.Http;
using Services.Interfaces;

namespace Services.Implementations;

public class FileService : IFileService
{
    private readonly IFileRepository _repo;

    public FileService(IFileRepository repo)
    {
        _repo = repo;
    }

    public async Task<FileModel> UploadFileAsync(IFormFile file)
    {
        var ext = Path.GetExtension(file.FileName);
        var id = Guid.NewGuid();
        var fileName = id + ext;
        var relativePath = Path.Combine("uploads", fileName);
        var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", relativePath);

        var dir = Path.GetDirectoryName(fullPath);
        if (!Directory.Exists(dir))
            Directory.CreateDirectory(dir!);

        using (var stream = new FileStream(fullPath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        var entity = new FileModel
        {
            Id = id,
            FileName = file.FileName,
            ContentType = file.ContentType ?? "application/octet-stream",
            Path = fullPath,
            Size = file.Length
        };

        return await _repo.AddAsync(entity);
    }

    public async Task<FileModel> UpdateFileAsync(Guid id, IFormFile file)
    {
        var entity = await _repo.GetByIdAsync(id);
        if (entity is null)
            throw new FileNotFoundException("Fayl topilmadi");

        if (System.IO.File.Exists(entity.Path))
            System.IO.File.Delete(entity.Path);

        var ext = Path.GetExtension(file.FileName);
        var fileName = id + ext;
        var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", fileName);

        using (var stream = new FileStream(fullPath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        entity.FileName = file.FileName;
        entity.ContentType = file.ContentType ?? "application/octet-stream";
        entity.Path = fullPath;
        entity.Size = file.Length;

        return await _repo.UpdateAsync(entity);
    }

    public async Task<FileModel> DeleteAsync(Guid id)
    {
        var entity = await _repo.GetByIdAsync(id);
        if (entity is null)
            throw new FileNotFoundException("Fayl topilmadi");

        if (System.IO.File.Exists(entity.Path))
            System.IO.File.Delete(entity.Path);

        await _repo.RemoveAsync(entity);
        return entity;
    }

    public async Task<FileModel> GetByIdAsync(Guid id)
    {
        var entity = await _repo.GetByIdAsync(id);
        if (entity is null)
            throw new FileNotFoundException("Fayl topilmadi");
        return entity;
    }

    public async Task<Stream> SendFileAsync(Guid id)
    {
        var file = await _repo.GetByIdAsync(id);
        if (file is null || !System.IO.File.Exists(file.Path))
            throw new FileNotFoundException("Fayl topilmadi");

        return new FileStream(file.Path, FileMode.Open, FileAccess.Read);
    }
}
