using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MovieMania.Core.Configurations.DTOs;

namespace MovieMania.Core.Entities;

public record PersonEntity : ContextBaseDTO
{
    [Key]
    [Column("person_id")]
    public int PersonId { get; set; }

    [Required]
    [StringLength(100)]
    [Column("name")]
    public string Name { get; set; }

    public ICollection<MovieCastEntity> MovieCasts { get; set; }
    public ICollection<MovieCrewEntity> MovieCrews { get; set; }
}