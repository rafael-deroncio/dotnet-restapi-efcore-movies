using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MovieMania.Domain.Requests;

public record KeywordRequest
{
    [Required]
    [StringLength(100)]
    [JsonPropertyName("keyword")]
    public string Keyword { get; set; }
}
