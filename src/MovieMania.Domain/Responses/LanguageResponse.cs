using System.Text.Json.Serialization;

namespace MovieMania.Domain.Responses;

public record LanguageResponse
{
    public int Id { get; set; }

    [JsonPropertyName("code")]
    public string Code { get; set; }

    [JsonPropertyName("language")]
    public string Language { get; set; }

    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; set; }
}
