using System.Text.Json.Serialization;

namespace MovieMania.Domain.Responses;

public record CastResponse
{
    [JsonPropertyName("character_name")]
    public string CharacterName { get; set; }

    [JsonPropertyName("cast_order")]
    public int CastOrder { get; set; }
    
    [JsonPropertyName("gender")]
    public GenderResponse Gender { get; set; }

    [JsonPropertyName("person")]
    public PersonResponse Person { get; set; }
}