using Core.Entities;
using Services.DTOs;

namespace Services.Mapping;

public static class FileMapper
{
    public static FileDto ToDto(FileModel f) => new()
    {
        Id = f.Id,
        FileName = f.FileName,
        ContentType = f.ContentType,
        Path = f.Path,
        Size = 0
    };
}
