using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MovieMania.Core.Configurations.DTOs;

namespace MovieMania.Core.Models;

public record GenreModel : ContextBaseDTO
{
    [Key]
    [Column("genre_id")]
    public int GenreId { get; set; }

    [Required]
    [StringLength(100)]
    [Column("name")]
    public string Name { get; set; }

    public ICollection<MovieGenreModel> MovieGenres { get; set; }
}
