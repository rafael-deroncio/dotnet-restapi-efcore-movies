using System.ComponentModel.DataAnnotations.Schema;
using MovieMania.Core.Configurations.DTOs;

namespace MovieMania.Core.Models;

public record MovieGenreModel : ContextBaseDTO
{
    [ForeignKey("Movie")]
    [Column("movie_id")]
    public int MovieId { get; set; }

    [ForeignKey("Genre")]
    [Column("genre_id")]
    public int GenreId { get; set; }

    public MovieModel Movie { get; set; }
    public GenreModel Genre { get; set; }
}
