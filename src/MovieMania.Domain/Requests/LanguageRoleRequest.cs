using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MovieMania.Domain.Requests;

/// <summary>
/// Represents a request to create or update a language role.
/// </summary>
public record LanguageRoleRequest
{
    /// <summary>
    /// Gets or sets the role of the language.
    /// </summary>
    [Required(ErrorMessage = "The role is required.")]
    [StringLength(100, ErrorMessage = "The role must be at most 100 characters long.")]
    [JsonPropertyName("role")]
    public string Role { get; set; }
}