using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MovieMania.Domain.Requests;

/// <summary>
/// Represents a request to create or update a country.
/// </summary>
public record CountryRequest
{
    /// <summary>
    /// Gets or sets the ISO code of the country.
    /// </summary>
    [Required(ErrorMessage = "The ISO code is required.")]
    [StringLength(2, ErrorMessage = "The ISO code must be exactly 2 characters long.")]
    [JsonPropertyName("iso_code")]
    public string IsoCode { get; set; }

    /// <summary>
    /// Gets or sets the name of the country.
    /// </summary>
    [Required(ErrorMessage = "The name is required.")]
    [StringLength(100, ErrorMessage = "The name must be at most 100 characters long.")]
    [JsonPropertyName("name")]
    public string Name { get; set; }
}