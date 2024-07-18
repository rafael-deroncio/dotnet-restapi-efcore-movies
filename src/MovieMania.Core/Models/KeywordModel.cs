using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MovieMania.Core.Configurations.DTOs;

namespace MovieMania.Core.Models;

public record KeywordModel : ContextBaseDTO
{
    [Key]
    [Column("keyword_id")]
    public int KeywordId { get; set; }

    [Required]
    [StringLength(100)]
    [Column("keyword_name")]
    public string Name { get; set; }

    public ICollection<MovieKeywordModel> MovieKeywords { get; set; }
}
