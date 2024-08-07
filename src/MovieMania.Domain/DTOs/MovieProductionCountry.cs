using MovieMania.Domain.Requests;

namespace MovieMania.Domain.DTOs;

public record MovieProductionCountry : ProductionCompanyRequest
{
    public int Id { get; set; }
}