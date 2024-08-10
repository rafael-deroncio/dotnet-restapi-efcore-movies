using System.Text.Json.Serialization;

namespace MovieMania.Domain.Responses;

public record ProductionCountryResponse 
{
    [JsonPropertyName("country")]
    public CountryResponse Country { get; set; }
}