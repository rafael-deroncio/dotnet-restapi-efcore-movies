using System.ComponentModel.DataAnnotations.Schema;
using MovieMania.Core.Configurations.DTOs;

namespace MovieMania.Core.Entities;

public record MovieLanguageEntity : EntityBase
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

    public MovieEntity Movie { get; set; }
    public LanguageEntity Language { get; set; }
    public LanguageRoleEntity LanguageRole { get; set; }
}