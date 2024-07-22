using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MovieMania.Core.Configurations.DTOs;

namespace MovieMania.Core.Entities;

public record GenderEntity : EntityBase
{
    [Key]
    [Column("gender_id")]
    public int GenderId { get; set; }

    [Required]
    [StringLength(50)]
    [Column("gender")]
    public string Gender { get; set; }

    public ICollection<MovieCastEntity> MovieCasts { get; set; }
}