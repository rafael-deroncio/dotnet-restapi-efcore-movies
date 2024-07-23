using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MovieMania.Core.Configurations.DTOs;

namespaceMovieMania.Core.Contexts.Entities;

public record LanguageRoleEntity : EntityBase
{
    [Key]
    [Column("role_id")]
    public int RoleId { get; set; }

    [Required]
    [StringLength(50)]
    [Column("role")]
    public string Role { get; set; }

    public ICollection<MovieLanguageEntity> MovieLanguages { get; set; }
}