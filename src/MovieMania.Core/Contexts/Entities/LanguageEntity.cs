using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MovieMania.Core.Configurations.DTOs;

namespace MovieMania.Core.Contexts.Entities;

public record LanguageEntity : EntityBase
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
    [Column("language")]
    public string Language { get; set; }

    public ICollection<MovieLanguageEntity> MovieLanguages { get; set; }
}