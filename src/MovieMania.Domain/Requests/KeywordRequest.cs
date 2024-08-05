using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MovieMania.Domain.Requests;

/// <summary>
/// Represents a request to create or update a keyword.
/// </summary>
public record KeywordRequest
{
    /// <summary>
    /// Gets or sets the keyword.
    /// </summary>
    [Required(ErrorMessage = "The keyword is required.")]
    [StringLength(100, ErrorMessage = "The keyword must be at most 100 characters long.")]
    [JsonPropertyName("keyword")]
    public string Keyword { get; set; }
}