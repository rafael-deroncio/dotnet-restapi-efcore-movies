using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MovieMania.Domain.Requests;

/// <summary>
/// Represents a request to create or update a gender.
/// </summary>
public record GenderRequest
{
    /// <summary>
    /// Gets or sets the gender.
    /// </summary>
    [Required(ErrorMessage = "The gender is required.")]
    [StringLength(100, ErrorMessage = "The gender must be at most 100 characters long.")]
    [JsonPropertyName("gender")]
    public string Gender { get; set; }
}