using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MovieMania.Core.Configurations.DTOs;

namespace MovieMania.Core.Models;

public record CountryModel : ContextBaseDTO
{
    [Key]
    [Column("country_id")]
    public int CountryId { get; set; }

    [Required]
    [StringLength(10)]
    [Column("iso_code")]
    public string IsoCode { get; set; }

    [Required]
    [StringLength(100)]
    [Column("name")]
    public string Name { get; set; }

    public ICollection<ProductionCountryModel> ProductionCountries { get; set; }
}