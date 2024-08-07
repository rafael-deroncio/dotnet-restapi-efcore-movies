using MovieMania.Domain.Requests;

namespace MovieMania.Domain.DTOs;

public record MovieCompanie : ProductionCompanyRequest
{
    public int Id { get; set; }
}