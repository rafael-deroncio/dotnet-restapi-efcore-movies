using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MovieMania.Domain.Requests;

public record LanguageRoleRequest
{
    [Required]
    [StringLength(100)]
    [JsonPropertyName("role")]
    public string Role { get; set; }
}
