using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MovieMania.Core.Configurations.DTOs;

namespace MovieMania.Core.Models;

public record PersonModel : ContextBaseDTO
{
    [Key]
    [Column("person_id")]
    public int PersonId { get; set; }

    [Required]
    [StringLength(100)]
    [Column("name")]
    public string Name { get; set; }

    public ICollection<MovieCastModel> MovieCasts { get; set; }
    public ICollection<MovieCrewModel> MovieCrews { get; set; }
}