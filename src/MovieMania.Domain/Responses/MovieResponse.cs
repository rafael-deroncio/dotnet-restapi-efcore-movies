using System.Text.Json.Serialization;

namespace MovieMania.Domain.Responses;

public record MovieResponse
{
    [JsonPropertyName("id")]
    public int MovieId { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; }

    [JsonPropertyName("budget")]
    public decimal Budget { get; set; }

    [JsonPropertyName("homepage")]
    public string Homepage { get; set; }

    [JsonPropertyName("overview")]
    public string Overview { get; set; }

    [JsonPropertyName("popularity")]
    public double Popularity { get; set; }

    [JsonPropertyName("release_date")]
    public DateTime ReleaseDate { get; set; }

    [JsonPropertyName("revenue")]
    public double Revenue { get; set; }

    [JsonPropertyName("runtime")]
    public int Runtime { get; set; }

    [JsonPropertyName("status")]
    public string Status { get; set; }

    [JsonPropertyName("tagline")]
    public string Tagline { get; set; }

    [JsonPropertyName("votes_avg")]
    public double VotesAverage { get; set; }

    [JsonPropertyName("votes_count")]
    public int VotesCount { get; set; }

    [JsonPropertyName("production_contries")]
    public List<ProductionCountryResponse> ProductionCountries { get; set; }

    [JsonPropertyName("companies")]
    public List<ProductionCompanyResponse> Companies { get; set; }
    
    [JsonPropertyName("languages")]
    public List<LanguageResponse> Languages { get; set; }

    [JsonPropertyName("genres")]
    public List<GenreResponse> Genres { get; set; }

    [JsonPropertyName("keywords")]
    public List<KeywordResponse> Keywords { get; set; }

    [JsonPropertyName("casts")]
    public List<CastResponse> Casts { get; set; }

    [JsonPropertyName("crews")]
    public List<CrewResponse> Crews { get; set; }

    [JsonPropertyName("images")]
    public List<ImageResponse> Images { get; set; }
}
