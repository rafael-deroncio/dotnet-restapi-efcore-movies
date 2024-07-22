using System.ComponentModel.DataAnnotations.Schema;
using MovieMania.Core.Configurations.DTOs;

namespace MovieMania.Core.Entities;

public record MovieKeywordEntity : EntityBase
{
    [ForeignKey("Movie")]
    [Column("movie_id")]
    public int MovieId { get; set; }

    [ForeignKey("Keyword")]
    [Column("keyword_id")]
    public int KeywordId { get; set; }

    public MovieEntity Movie { get; set; }
    public KeywordEntity Keyword { get; set; }
}
