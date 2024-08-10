using MovieMania.Domain.Requests;

namespace MovieMania.Domain.Requests;

public record MovieProductionCountryRequest
{
    public MovieCountryRequest Country { get; set; }
}

public record MovieCountryRequest
{
    public int Id { get; set; }
}