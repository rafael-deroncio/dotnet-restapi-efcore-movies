using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MovieMania.Core.Configurations.DTOs;

namespace MovieMania.Core.Models;

public record LanguageRoleModel : ContextBaseDTO
{
    [Key]
    [Column("role_id")]
    public int RoleId { get; set; }

    [Required]
    [StringLength(50)]
    [Column("role")]
    public string Role { get; set; }

    public ICollection<MovieLanguageModel> MovieLanguages { get; set; }
}