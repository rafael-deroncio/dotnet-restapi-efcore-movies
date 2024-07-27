using System.Text.Json.Serialization;

namespace MovieMania.Domain.Responses;

public record CountryResponse
{
    public int Id { get; set; }

    [JsonPropertyName("iso_code")]
    public string IsoCode { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; set; }
}
