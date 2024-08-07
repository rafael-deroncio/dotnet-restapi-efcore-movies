using MovieMania.Domain.Requests;

namespace MovieMania.Domain.DTOs;

public record MoviePerson : PersonRequest
{
    public int Id { get; set; }
}
