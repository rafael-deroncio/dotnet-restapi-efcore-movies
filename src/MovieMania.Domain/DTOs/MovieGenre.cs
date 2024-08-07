using MovieMania.Domain.Requests;

namespace MovieMania.Domain.DTOs;

public record MovieGenre : GenreRequest
{
    public int Id { get; set; }
}
