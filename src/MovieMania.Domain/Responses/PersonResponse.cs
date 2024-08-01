using System.Text.Json.Serialization;

namespace MovieMania.Domain.Responses;

public record PersonResponse
{
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; set; }
}
