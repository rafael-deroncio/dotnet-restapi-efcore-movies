using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MovieMania.Core.Configurations.DTOs;

namespace MovieMania.Core.Entities;

public record LanguageEntity : ContextBaseDTO
{
    [Key]
    [Column("language_id")]
    public int LanguageId { get; set; }

    [Required]
    [StringLength(10)]
    [Column("code")]
    public string Code { get; set; }

    [Required]
    [StringLength(100)]
    [Column("name")]
    public string Name { get; set; }

    public ICollection<MovieLanguageEntity> MovieLanguages { get; set; }
}