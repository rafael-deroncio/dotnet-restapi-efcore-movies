using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MovieMania.Core.Configurations.DTOs;

namespace MovieMania.Core.Entities;

public record GenreEntity : ContextBaseDTO
{
    [Key]
    [Column("genre_id")]
    public int GenreId { get; set; }

    [Required]
    [StringLength(100)]
    [Column("name")]
    public string Name { get; set; }

    public ICollection<MovieGenreEntity> MovieGenres { get; set; }
}
