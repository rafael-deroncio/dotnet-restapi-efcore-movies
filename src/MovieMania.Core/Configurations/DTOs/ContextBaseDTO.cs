using System.ComponentModel.DataAnnotations.Schema;

namespace MovieMania.Core.Configurations.DTOs;

public record EntityBase
{
    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}