using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MovieMania.Core.Configurations.DTOs;

namespace MovieMania.Core.Contexts.Entities;

public record PersonEntity : EntityBase
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