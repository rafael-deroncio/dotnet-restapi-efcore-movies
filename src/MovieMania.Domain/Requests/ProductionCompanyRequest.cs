using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MovieMania.Domain.Requests;

public record ProductionCompanyRequest
{
    [Required]
    [StringLength(100)]
    [JsonPropertyName("company")]
    public string Company { get; set; }
}
