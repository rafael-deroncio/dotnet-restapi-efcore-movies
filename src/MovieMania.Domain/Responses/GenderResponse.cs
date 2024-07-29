using System.Text.Json.Serialization;

namespace MovieMania.Domain.Responses;

public record GenderResponse
{
    public int Id { get; set; }

    [JsonPropertyName("gender")]
    public string Gender { get; set; }

    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; set; }
}
