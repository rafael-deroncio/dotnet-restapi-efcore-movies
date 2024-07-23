using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieMania.Core.Configurations.DTOs;

public record EntityBase
{
    [Column("created_at")]
    [Required]
    public DateTime CreatedAt { get; set; }

    [Column("updated_at")]
    [Required]
    public DateTime UpdatedAt { get; set; }
}