using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MovieMania.Domain.Requests;

/// <summary>
/// Represents a request to create or update a production company.
/// </summary>
public record ProductionCompanyRequest
{
    /// <summary>
    /// Gets or sets the name of the production company.
    /// </summary>
    [Required(ErrorMessage = "The company name is required.")]
    [StringLength(100, ErrorMessage = "The company name must be at most 100 characters long.")]
    [JsonPropertyName("company")]
    public string Company { get; set; }
}