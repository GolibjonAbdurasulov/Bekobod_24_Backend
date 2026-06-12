using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core;

public class ModelBase<TId>
{
    [Key]
    [Column("id")]
    public TId Id { get; set; } = default!;
}
