using System.ComponentModel.DataAnnotations.Schema;
using MovieMania.Core.Configurations.DTOs;

namespace MovieMania.Core.Entities;

public record MovieCompanyEntity : ContextBaseDTO
{
    [ForeignKey("Movie")]
    [Column("movie_id")]
    public int MovieId { get; set; }

    [ForeignKey("ProductionCompany")]
    [Column("company_id")]
    public int CompanyId { get; set; }

    public MovieModel Movie { get; set; }
    public ProductionCompanyModel ProductionCompany { get; set; }
}