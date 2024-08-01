using System.Text.Json.Serialization;

namespace MovieMania.Domain.Responses;

public record ProductionCompanyResponse
{
    public int Id { get; set; }

    [JsonPropertyName("company")]
    public string Company { get; set; }

    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; set; }
}
