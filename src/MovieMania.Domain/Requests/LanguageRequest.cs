using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MovieMania.Domain.Requests;

public record LanguageRequest
{
    [Required]
    [StringLength(5)]
    [JsonPropertyName("code")]
    public string Code { get; set; }

    [Required]
    [StringLength(100)]
    [JsonPropertyName("language")]
    public string Language { get; set; }
}
