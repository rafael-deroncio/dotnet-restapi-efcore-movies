using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MovieMania.Core.Configurations.DTOs;

namespace MovieMania.Core.Contexts.Entities;

public record ProductionCompanyEntity : EntityBase
{
    [Key]
    [Column("company_id")]
    public int CompanyId { get; set; }

    [Required]
    [StringLength(100)]
    [Column("name")]
    public string Name { get; set; }

    public ICollection<MovieCompanyEntity> MovieCompanies { get; set; }
}