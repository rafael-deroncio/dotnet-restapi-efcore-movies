using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MovieMania.Core.Configurations.DTOs;

namespace MovieMania.Core.Contexts.Entities;

public record LanguageRoleEntity : EntityBase
{
    [Key]
    [Column("language_role_id")]
    public int LanguageRoleId { get; set; }

    [Required]
    [StringLength(50)]
    [Column("role")]
    public string Role { get; set; }

    public ICollection<MovieLanguageEntity> MovieLanguages { get; set; }
}