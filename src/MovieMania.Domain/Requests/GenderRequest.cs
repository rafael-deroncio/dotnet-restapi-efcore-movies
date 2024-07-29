using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MovieMania.Domain.Requests;

public record GenderRequest
{
    [Required]
    [StringLength(100)]
    [JsonPropertyName("gender")]
    public string Gender { get; set; }
}
