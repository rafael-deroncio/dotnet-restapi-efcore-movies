using System.Text.Json.Serialization;

namespace MovieMania.Domain.Responses;

public record KeywordResponse
{
    public int Id { get; set; }

    [JsonPropertyName("keyword")]
    public string Keyword { get; set; }

    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; set; }
}
