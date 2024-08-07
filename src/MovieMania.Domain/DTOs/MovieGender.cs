using MovieMania.Domain.Requests;

namespace MovieMania.Domain.DTOs;

public record MovieGender : GenderRequest
{
    public int Id { get; set; }
}
