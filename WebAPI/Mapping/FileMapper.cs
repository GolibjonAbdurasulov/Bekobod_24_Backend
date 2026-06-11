using Core.Entities;
using WebAPI.DTOs;

namespace WebAPI.Mapping;

public static class FileMapper
{
    public static FileDto ToDto(FileModel file)
    {
        if (file == null) return null;

        return new FileDto
        {
            Id = file.Id,
            FileName = file.FileName,
            ContentType = file.ContentType,
            Path = file.Path
        };
    }

    public static List<FileDto> ToDtoList(List<FileModel> files)
    {
        return files.Select(ToDto).ToList();
    }
}