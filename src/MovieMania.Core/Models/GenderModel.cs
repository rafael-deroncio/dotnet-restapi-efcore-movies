using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MovieMania.Core.Configurations.DTOs;

namespace MovieMania.Core.Models;

public record GenderModel : ContextBaseDTO
{
    [Key]
    [Column("gender_id")]
    public int GenderId { get; set; }

    [Required]
    [StringLength(50)]
    [Column("gender")]
    public string Gender { get; set; }

    public ICollection<MovieCastModel> MovieCasts { get; set; }
}