using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MovieMania.Core.Configurations.DTOs;

namespace MovieMania.Core.Entities;

public record CountryEntity : ContextBaseDTO
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

    public ICollection<ProductionCountryEntity> ProductionCountries { get; set; }
}