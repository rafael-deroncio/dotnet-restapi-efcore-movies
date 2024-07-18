using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MovieMania.Core.Configurations.DTOs;

namespace MovieMania.Core.Models;

public record DepartmentModel : ContextBaseDTO
{
    [Key]
    [Column("department_id")]
    public int DepartmentId { get; set; }

    [Required]
    [StringLength(100)]
    [Column("department_name")]
    public string Name { get; set; }

    public ICollection<MovieCrewModel> MovieCrews { get; set; }
}

