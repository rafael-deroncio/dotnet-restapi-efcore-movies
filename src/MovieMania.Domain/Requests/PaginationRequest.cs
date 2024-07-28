using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MovieMania.Domain.Requests;

/// <summary>
/// Represents a request for paginated data.
/// </summary>
public record PaginationRequest
{
    private int _page;
    private int _size;

    /// <summary>
    /// Gets or sets the page number for the paginated request.
    /// This property maps to the JSON property 'page'.
    /// </summary>
    [JsonPropertyName("page")]
    public int Page
    {
        get => _page;
        set => _page = value <= 0 ? 1 : value;
    }


    /// <summary>
    /// Gets or sets the size of the page (number of items per page) for the paginated request.
    /// This property maps to the JSON property 'size'.
    /// </summary>
    [JsonPropertyName("size")]
    public int Size
    {
        get => _size;
        set => _size =  value <= 0 ? 1 :
                        value > 10 ? 10 : value;
    }
}