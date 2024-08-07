namespace MovieMania.Domain.DTOs;

public record MovieCrew
{
    public MoviePerson Person { get; set; }
    public MovieDepartment Department { get; set; }
}
