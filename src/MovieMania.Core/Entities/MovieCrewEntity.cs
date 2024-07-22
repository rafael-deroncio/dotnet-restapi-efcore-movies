using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MovieMania.Core.Configurations.DTOs;

namespace MovieMania.Core.Entities;

public record MovieCrewEntity : ContextBaseDTO
{
    [ForeignKey("Movie")]
    [Column("movie_id")]
    public int MovieId { get; set; }

    [ForeignKey("Person")]
    [Column("person_id")]
    public int PersonId { get; set; }

    [ForeignKey("Department")]
    [Column("department_id")]
    public int DepartmentId { get; set; }

    [StringLength(255)]
    [Column("job")]
    public string Job { get; set; }

    public MovieModel Movie { get; set; }
    public PersonModel Person { get; set; }
    public DepartmentModel Department { get; set; }
}

