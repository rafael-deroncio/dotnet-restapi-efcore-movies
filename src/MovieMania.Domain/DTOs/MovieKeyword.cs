using MovieMania.Domain.Requests;

namespace MovieMania.Domain.DTOs;

public record MovieKeyword : KeywordRequest
{
    public int Id { get; set; }
}
