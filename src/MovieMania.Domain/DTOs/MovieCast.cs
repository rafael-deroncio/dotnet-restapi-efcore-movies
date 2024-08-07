namespace MovieMania.Domain.DTOs;

public record MovieCast
{
    public MoviePerson Person { get; set; }
    public MovieGender Gender { get; set; }
}