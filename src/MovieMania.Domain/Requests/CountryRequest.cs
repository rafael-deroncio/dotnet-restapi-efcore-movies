using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MovieMania.Domain.Requests;

public record CountryRequest
{
    [Required]
    [StringLength(10)]
    [JsonPropertyName("iso_code")]
    public string IsoCode { get; set; }

    [Required]
    [StringLength(100)]
    [JsonPropertyName("name")]
    public string Name { get; set; }
}
