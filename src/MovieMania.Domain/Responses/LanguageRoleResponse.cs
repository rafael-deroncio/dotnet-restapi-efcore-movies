using System.Text.Json.Serialization;

namespace MovieMania.Domain.Responses;

public record LanguageRoleResponse
{
    public int Id { get; set; }

    [JsonPropertyName("role")]
    public string Role { get; set; }

    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; set; }
}
