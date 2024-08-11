using System.Text.Json.Serialization;

namespace MovieMania.Domain.Requests;

/// <summary>
/// Represents a request to filter the additional information to be included in a movie response.
/// </summary>
public class MovieFilterRequest
{
    /// <summary>
    /// Gets or sets a value indicating whether to include production countries in the response.
    /// </summary>
    [JsonPropertyName("add_production_contries")]
    public bool AddProductionCountries { get; set; } = false;

    /// <summary>
    /// Gets or sets a value indicating whether to include companies in the response.
    /// </summary>
    [JsonPropertyName("add_companies")]
    public bool AddCompanies { get; set; } = false;

    /// <summary>
    /// Gets or sets a value indicating whether to include languages in the response.
    /// </summary>
    [JsonPropertyName("add_languages")]
    public bool AddLanguages { get; set; } = false;

    /// <summary>
    /// Gets or sets a value indicating whether to include genres in the response.
    /// </summary>
    [JsonPropertyName("add_genres")]
    public bool AddGenres { get; set; } = false;

    /// <summary>
    /// Gets or sets a value indicating whether to include keywords in the response.
    /// </summary>
    [JsonPropertyName("add_keywords")]
    public bool AddKeywords { get; set; } = false;

    /// <summary>
    /// Gets or sets a value indicating whether to include cast members in the response.
    /// </summary>
    [JsonPropertyName("add_casts")]
    public bool AddCasts { get; set; } = false;

    /// <summary>
    /// Gets or sets a value indicating whether to include crew members in the response.
    /// </summary>
    [JsonPropertyName("add_crews")]
    public bool AddCrews { get; set; } = false;

    /// <summary>
    /// Gets or sets a value indicating whether to include images in the response.
    /// </summary>
    [JsonPropertyName("add_images")]
    public bool AddImages { get; set; } = false;
}
