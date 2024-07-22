using System.Text.Json.Serialization;

namespace MovieMania.Domain.Responses;

/// <summary>
/// Represents a paginated response containing data of type <typeparamref name="T"/>.
/// </summary>
/// <typeparam name="T">The type of the data contained in the response.</typeparam>
public class PaginationResponse<T>
{
    /// <summary>
    /// Gets or sets the current page number.
    /// This property maps to the JSON property 'page'.
    /// </summary>
    [JsonPropertyName("page")]
    public int PageNumber { get; set; }

    /// <summary>
    /// Gets or sets the size of the page (number of items per page).
    /// This property maps to the JSON property 'size'.
    /// </summary>
    [JsonPropertyName("size")]
    public int PageSize { get; set; }

    /// <summary>
    /// Gets or sets the URI for the first page.
    /// This property maps to the JSON property 'first'.
    /// </summary>
    [JsonPropertyName("first")]
    public Uri FirstPage { get; set; }

    /// <summary>
    /// Gets or sets the URI for the last page.
    /// This property maps to the JSON property 'last'.
    /// </summary>
    [JsonPropertyName("last")]
    public Uri LastPage { get; set; }

    /// <summary>
    /// Gets or sets the total number of pages.
    /// This property maps to the JSON property 'pages'.
    /// </summary>
    [JsonPropertyName("pages")]
    public int TotalPages { get; set; }

    /// <summary>
    /// Gets or sets the total number of records.
    /// This property maps to the JSON property 'records'.
    /// </summary>
    [JsonPropertyName("records")]
    public int TotalRecords { get; set; }

    /// <summary>
    /// Gets or sets the URI for the next page.
    /// This property maps to the JSON property 'next'.
    /// </summary>
    [JsonPropertyName("next")]
    public Uri NextPage { get; set; }

    /// <summary>
    /// Gets or sets the URI for the previous page.
    /// This property maps to the JSON property 'previous'.
    /// </summary>
    [JsonPropertyName("previous")]
    public Uri PreviousPage { get; set; }

    /// <summary>
    /// Gets or sets the data for the current page.
    /// This property maps to the JSON property 'data'.
    /// </summary>
    [JsonPropertyName("data")]
    public IEnumerable<T> Data { get; set; }
}
