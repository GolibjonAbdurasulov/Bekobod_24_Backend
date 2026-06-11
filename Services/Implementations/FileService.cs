using Core.Entities;
using Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Services.Interfaces;

namespace Services.Implementations;

public class FileService : IFileService
{
    private readonly AppDbContext _db;
    private const string UploadsFolder = "wwwroot/uploads";

    public FileService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<FileModel> UploadAsync(IFormFile file)
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

        var fileModel = new FileModel
        {
            Id = id,
            FileName = file.FileName,
            ContentType = file.ContentType ?? "application/octet-stream",
            Path = relativePath
        };

        _db.Add(fileModel);
        await _db.SaveChangesAsync();

        return fileModel;
    }

    public async Task<FileModel> UpdateAsync(Guid id, IFormFile file)
    {
        var existing = await _db.Files.FindAsync(id)
            ?? throw new FileNotFoundException("Fayl topilmadi");

        var oldFullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", existing.Path);
        if (File.Exists(oldFullPath))
            File.Delete(oldFullPath);

        var ext = Path.GetExtension(file.FileName);
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

        existing.FileName = file.FileName;
        existing.ContentType = file.ContentType ?? "application/octet-stream";
        existing.Path = relativePath;

        await _db.SaveChangesAsync();
        return existing;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var file = await _db.Files.FindAsync(id)
            ?? throw new FileNotFoundException("Fayl topilmadi");

        var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", file.Path);
        if (File.Exists(fullPath))
            File.Delete(fullPath);

        _db.Files.Remove(file);
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<FileModel> GetByIdAsync(Guid id)
    {
        return await _db.Files.FindAsync(id)
            ?? throw new FileNotFoundException("Fayl topilmadi");
    }

    public async Task<Stream> GetStreamAsync(Guid id)
    {
        var file = await _db.Files.FindAsync(id)
            ?? throw new FileNotFoundException("Fayl topilmadi");

        var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", file.Path);
        if (!File.Exists(fullPath))
            throw new FileNotFoundException("Fayl diskda topilmadi");

        return new FileStream(fullPath, FileMode.Open, FileAccess.Read);
    }
}
