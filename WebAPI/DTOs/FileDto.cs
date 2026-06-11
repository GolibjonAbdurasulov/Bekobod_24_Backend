namespace WebAPI.DTOs;

public class FileDto
{
    public Guid Id { get; set; }

    public string FileName { get; set; }

    public string ContentType { get; set; }

    public string Path { get; set; }

    public long Size { get; set; }

    public string Url => Path; // frontend uchun qulay
}