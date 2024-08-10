using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieMania.Core.Configurations.DTOs;

public record EntityBase
{
    [Column("created_at", TypeName = "Date")]
    [DataType(DataType.Date)]
    [Required]
    public DateTime CreatedAt { get; set; }

    [Column("updated_at", TypeName = "Date")]
    [DataType(DataType.Date)]
    [Required]
    public DateTime UpdatedAt { get; set; }
}