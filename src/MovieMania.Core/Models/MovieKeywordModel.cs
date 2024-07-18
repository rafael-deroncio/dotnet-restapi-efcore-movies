using System.ComponentModel.DataAnnotations.Schema;
using MovieMania.Core.Configurations.DTOs;

namespace MovieMania.Core.Models;

public record MovieKeywordModel : ContextBaseDTO
{
    [ForeignKey("Movie")]
    [Column("movie_id")]
    public int MovieId { get; set; }

    [ForeignKey("Keyword")]
    [Column("keyword_id")]
    public int KeywordId { get; set; }

    public MovieModel Movie { get; set; }
    public KeywordModel Keyword { get; set; }
}
