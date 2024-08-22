using System.Text.Json.Serialization;

namespace MovieMania.Domain.Responses;

public record ImageResponse
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("image_url")]
    public string ImageUrl { get; set; }
}