using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MovieMania.Core.Configurations.DTOs;

namespace MovieMania.Core.Models;

public record ProductionCountryModel : ContextBaseDTO
{
    [ForeignKey("Movie")]
    [Column("movie_id")]
    public int MovieId { get; set; }

    [ForeignKey("Country")]
    [Column("country_id")]
    public int CountryId { get; set; }

    public MovieModel Movie { get; set; }
    public CountryModel Country { get; set; }
}