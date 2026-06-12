using API.Common;
using Core.Entities;
using Infrastructure.Repositories.FileRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class FileController : ControllerBase
{
    private readonly IFileRepository _fileRepository;
    private readonly IFileService _fileService;

    public FileController(IFileRepository fileRepository, IFileService fileService)
    {
        _fileRepository = fileRepository;
        _fileService = fileService;
    }

    [HttpPost]
    public async Task<ResponseModelBase> UploadFileAsync(IFormFile file)
    {
        var result = await _fileService.UploadFileAsync(file);
        return new ResponseModelBase(result, 200);
    }

    [HttpPut]
    public async ValueTask<ResponseModelBase> ReplaceFileAsync(Guid id, IFormFile file)
    {
        var result = await _fileService.UpdateFileAsync(id, file);
        return new ResponseModelBase(result, 200);
    }

    [HttpDelete]
    public async ValueTask<ResponseModelBase> DeleteAsync(Guid id)
    {
        var result = await _fileService.DeleteAsync(id);
        return new ResponseModelBase(result, 200);
    }

    [HttpGet("download/{id}")]
    [AllowAnonymous]
    public async Task<IActionResult> DownloadFileAsync(Guid id)
    {
        try
        {
            var stream = await _fileService.SendFileAsync(id);
            var file = await _fileRepository.GetByIdAsync(id);
            var contentType = "application/octet-stream";
            var fileName = Path.GetFileName(file.Path);

            return File(stream, contentType, fileName);
        }
        catch (FileNotFoundException)
        {
            return NotFound();
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Ichki server xatosi: " + ex.Message);
        }
    }

    [HttpPost]
    [RequestSizeLimit(300_000_000)]
    [RequestFormLimits(MultipartBodyLengthLimit = 300_000_000)]
    public async Task<ResponseModelBase> UploadLargeFileAsync(IFormFile file)
    {
        if (file == null)
            return new ResponseModelBase("Fayl yuborilmadi", System.Net.HttpStatusCode.BadRequest);

        try
        {
            var result = await _fileService.UploadFileAsync(file);
            return new ResponseModelBase(result);
        }
        catch (Exception ex)
        {
            return new ResponseModelBase(ex);
        }
    }
}
