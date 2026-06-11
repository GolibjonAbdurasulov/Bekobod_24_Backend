using Core.Entities;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/files")]
public class FileController : ControllerBase
{
    private readonly AppDbContext _db;

    public FileController(AppDbContext db)
    {
        _db = db;
    }

    [HttpPost("upload")]
    public async Task<FileModel> Upload(IFormFile file)
    {
        var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
        var path = Path.Combine("uploads", fileName);

        using (var stream = new FileStream(path, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        var entity = new FileModel
        {
            FileName = file.FileName,
            ContentType = file.ContentType,
            Path = path,
            Size = file.Length
        };

        _db.Files.Add(entity);
        await _db.SaveChangesAsync();

        return entity;
    }

    [HttpGet("{id}")]
    public async Task<FileModel?> Get(long id)
    {
        return await _db.Files.FindAsync(id);
    }
}