using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MovieMania.Domain.Requests;

/// <summary>
/// Represents a request to create or update a genre.
/// </summary>
public record GenreRequest
{
    /// <summary>
    /// Gets or sets the name of the genre.
    /// </summary>
    [Required(ErrorMessage = "The name is required.")]
    [StringLength(100, ErrorMessage = "The name must be at most 100 characters long.")]
    [JsonPropertyName("name")]
    public string Name { get; set; }
}