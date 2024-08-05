using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MovieMania.Domain.Requests;

/// <summary>
/// Represents a request to create or update a language.
/// </summary>
public record LanguageRequest
{
    /// <summary>
    /// Gets or sets the code of the language.
    /// </summary>
    [Required(ErrorMessage = "The code is required.")]
    [StringLength(5, ErrorMessage = "The code must be at most 5 characters long.")]
    [JsonPropertyName("code")]
    public string Code { get; set; }

    /// <summary>
    /// Gets or sets the name of the language.
    /// </summary>
    [Required(ErrorMessage = "The language is required.")]
    [StringLength(100, ErrorMessage = "The language must be at most 100 characters long.")]
    [JsonPropertyName("language")]
    public string Language { get; set; }
}