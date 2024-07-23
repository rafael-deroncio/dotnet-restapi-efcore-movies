using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MovieMania.Core.Configurations.DTOs;

namespaceMovieMania.Core.Contexts.Entities;

public record ProductionCountryEntity : EntityBase
{
    [ForeignKey("Movie")]
    [Column("movie_id")]
    public int MovieId { get; set; }

    [ForeignKey("Country")]
    [Column("country_id")]
    public int CountryId { get; set; }

    public MovieEntity Movie { get; set; }
    public CountryEntity Country { get; set; }
}