using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MovieMania.Core.Configurations.DTOs;

namespace MovieMania.Core.Models;

public record ProductionCompanyModel : ContextBaseDTO
{
    [Key]
    [Column("company_id")]
    public int CompanyId { get; set; }

    [Required]
    [StringLength(100)]
    [Column("name")]
    public string Name { get; set; }

    public ICollection<MovieCompanyModel> MovieCompanies { get; set; }
}