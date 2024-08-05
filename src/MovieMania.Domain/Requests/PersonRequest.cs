using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MovieMania.Domain.Requests;

/// <summary>
/// Represents a request to create or update a person.
/// </summary>
public record PersonRequest
{
    /// <summary>
    /// Gets or sets the name of the person.
    /// </summary>
    [Required(ErrorMessage = "The name is required.")]
    [StringLength(100, ErrorMessage = "The name must be at most 100 characters long.")]
    [JsonPropertyName("name")]
    public string Name { get; set; }
}