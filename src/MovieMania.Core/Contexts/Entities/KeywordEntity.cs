using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MovieMania.Core.Configurations.DTOs;

namespace MovieMania.Core.Contexts.Entities;

public record KeywordEntity : EntityBase
{
    [Key]
    [Column("keyword_id")]
    public int KeywordId { get; set; }

    [Required]
    [StringLength(100)]
    [Column("keyword_name")]
    public string Name { get; set; }

    public ICollection<MovieKeywordEntity> MovieKeywords { get; set; }
}
