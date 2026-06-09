using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/files")]
public class FilesController : ControllerBase
{
    private readonly IFileService _fileService;

    public FilesController(IFileService fileService)
    {
        _fileService = fileService;
    }

    [HttpPost("upload")]
    [RequestSizeLimit(50_000_000)]
    public async Task<IActionResult> Upload(IFormFile file)
    {
        if (file == null || file.Length == 0)
            return BadRequest(new { error = "Fayl yuborilmadi" });

        var result = await _fileService.UploadAsync(file);
        return Ok(new
        {
            id = result.Id,
            fileName = result.FileName,
            contentType = result.ContentType,
            url = $"{Request.Scheme}://{Request.Host}/api/files/download/{result.Id}"
        });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, IFormFile file)
    {
        try
        {
            var result = await _fileService.UpdateAsync(id, file);
            return Ok(new
            {
                id = result.Id,
                fileName = result.FileName,
                contentType = result.ContentType,
                url = $"{Request.Scheme}://{Request.Host}/api/files/download/{result.Id}"
            });
        }
        catch (FileNotFoundException)
        {
            return NotFound(new { error = "Fayl topilmadi" });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            await _fileService.DeleteAsync(id);
            return NoContent();
        }
        catch (FileNotFoundException)
        {
            return NotFound(new { error = "Fayl topilmadi" });
        }
    }

    [HttpGet("download/{id}")]
    public async Task<IActionResult> Download(Guid id)
    {
        try
        {
            var file = await _fileService.GetByIdAsync(id);
            var stream = await _fileService.GetStreamAsync(id);
            return File(stream, file.ContentType, file.FileName);
        }
        catch (FileNotFoundException)
        {
            return NotFound();
        }
    }
}
