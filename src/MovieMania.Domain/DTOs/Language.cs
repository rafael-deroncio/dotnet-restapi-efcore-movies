using MovieMania.Domain.Requests;

namespace MovieMania.Domain.DTOs;

public record Language : LanguageRequest
{
    public int Id { get; set; }
}
