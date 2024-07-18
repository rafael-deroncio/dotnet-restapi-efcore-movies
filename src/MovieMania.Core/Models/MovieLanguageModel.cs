using System.ComponentModel.DataAnnotations.Schema;
using MovieMania.Core.Configurations.DTOs;

namespace MovieMania.Core.Models;

public record MovieLanguageModel : ContextBaseDTO
{
    [ForeignKey("Movie")]
    [Column("movie_id")]
    public int MovieId { get; set; }

    [ForeignKey("Language")]
    [Column("language_id")]
    public int LanguageId { get; set; }

    [ForeignKey("LanguageRole")]
    [Column("language_role_id")]
    public int LanguageRoleId { get; set; }

    public MovieModel Movie { get; set; }
    public LanguageModel Language { get; set; }
    public LanguageRoleModel LanguageRole { get; set; }
}