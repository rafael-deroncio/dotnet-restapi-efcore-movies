using System.Text.Json.Serialization;

namespace MovieMania.Domain.Requests;

/// <summary>
/// Represents a request for paginated data.
/// </summary>
public record PaginationRequest
{
    /// <summary>
    /// Gets or sets the page number for the paginated request.
    /// This property maps to the JSON property 'page'.
    /// </summary>
    [JsonPropertyName("page")]
    public int Page { get; set; }

    /// <summary>
    /// Gets or sets the size of the page (number of items per page) for the paginated request.
    /// This property maps to the JSON property 'size'.
    /// </summary>
    [JsonPropertyName("size")]
    public int Size { get; set; }
}