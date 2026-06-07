using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Models.File;

[Table("file_model")]
public class FileModel 
{
    [Column($"file_id")] public Guid FileId { get; set; }
    [Column("file_name")] public string FileName { get; set; }
    [Column("content_type")] public string ContentType { get; set; }
    [Column("path")] public string Path { get; set; }
}