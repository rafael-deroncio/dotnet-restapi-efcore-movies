namespace MovieMania.Core.Configurations.DTOs;

public record ContextBaseDTO
{
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}