using System.Text.Json.Serialization;

namespace MovieMania.Domain.Responses;

public record MovieLanguageResponse
{
    [JsonPropertyName("language")]
    public LanguageResponse Language { get; set; }
    [JsonPropertyName("language_role")]
    public LanguageRoleResponse LanguageRole { get; set; }
}
