using MovieMania.Domain.Requests;

namespace MovieMania.Domain.DTOs;

public record LanguageRole : LanguageRoleRequest
{
    public int Id { get; set; }
}
