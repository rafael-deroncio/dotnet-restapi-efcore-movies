using System.ComponentModel.DataAnnotations.Schema;
using MovieMania.Core.Configurations.DTOs;

namespace MovieMania.Core.Contexts.Entities;

public record MovieCompanyEntity : EntityBase
{
    [ForeignKey("Movie")]
    [Column("movie_id")]
    public int MovieId { get; set; }

    [ForeignKey("ProductionCompany")]
    [Column("company_id")]
    public int CompanyId { get; set; }

    public MovieEntity Movie { get; set; }
    public ProductionCompanyEntity ProductionCompany { get; set; }
}