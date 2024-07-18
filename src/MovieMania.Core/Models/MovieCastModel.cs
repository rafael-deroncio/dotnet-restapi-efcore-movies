using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MovieMania.Core.Configurations.DTOs;

namespace MovieMania.Core.Models;

public record MovieCastModel : ContextBaseDTO
{
    [ForeignKey("Movie")]
    [Column("movie_id")]
    public int MovieId { get; set; }

    [ForeignKey("Gender")]
    [Column("gender_id")]
    public int GenderId { get; set; }

    [ForeignKey("Person")]
    [Column("person_id")]
    public int PersonId { get; set; }

    [StringLength(255)]
    [Column("character_name")]
    public string CharacterName { get; set; }

    [Column("cast_order")]
    public int CastOrder { get; set; }

    public MovieModel Movie { get; set; }
    public GenderModel Gender { get; set; }
    public PersonModel Person { get; set; }
}
