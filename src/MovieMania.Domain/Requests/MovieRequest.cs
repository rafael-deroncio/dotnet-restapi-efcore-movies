using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using MovieMania.Domain.DTOs;

namespace MovieMania.Domain.Requests;

/// <summary>
/// Represents a request to create or update a movie.
/// </summary>
public class MovieRequest
{
    /// <summary>
    /// Gets or sets the title of the movie.
    /// </summary>
    [Required(ErrorMessage = "The title is required.")]
    [StringLength(100, ErrorMessage = "The title must be at most 100 characters long.")]
    [JsonPropertyName("title")]
    public string Title { get; set; }

    /// <summary>
    /// Gets or sets the budget of the movie.
    /// </summary>
    [Required(ErrorMessage = "The budget is required.")]
    [Range(0, double.MaxValue, ErrorMessage = "The budget must be a positive value.")]
    [JsonPropertyName("budget")]
    public decimal Budget { get; set; }

    /// <summary>
    /// Gets or sets the homepage URL of the movie.
    /// </summary>
    [Required(ErrorMessage = "The homepage is required.")]
    [Url(ErrorMessage = "The homepage must be a valid URL.")]
    [JsonPropertyName("homepage")]
    public string Homepage { get; set; }

    /// <summary>
    /// Gets or sets the overview of the movie.
    /// </summary>
    [Required(ErrorMessage = "The overview is required.")]
    [StringLength(1000, ErrorMessage = "The overview must be at most 1000 characters long.")]
    [JsonPropertyName("overview")]
    public string Overview { get; set; }

    /// <summary>
    /// Gets or sets the popularity of the movie.
    /// </summary>
    [Required(ErrorMessage = "The popularity is required.")]
    [Range(0, double.MaxValue, ErrorMessage = "The popularity must be a positive value.")]
    [JsonPropertyName("popularity")]
    public double Popularity { get; set; }

    /// <summary>
    /// Gets or sets the release date of the movie.
    /// </summary>
    [Required(ErrorMessage = "The release date is required.")]
    [DataType(DataType.Date, ErrorMessage = "The release date must be a valid date.")]
    [JsonPropertyName("release_date")]
    public DateTime ReleaseDate { get; set; }

    /// <summary>
    /// Gets or sets the revenue of the movie.
    /// </summary>
    [Required(ErrorMessage = "The revenue is required.")]
    [Range(0, double.MaxValue, ErrorMessage = "The revenue must be a positive value.")]
    [JsonPropertyName("revenue")]
    public double Revenue { get; set; }

    /// <summary>
    /// Gets or sets the runtime of the movie in minutes.
    /// </summary>
    [Required(ErrorMessage = "The runtime is required.")]
    [Range(0, int.MaxValue, ErrorMessage = "The runtime must be a positive value.")]
    [JsonPropertyName("runtime")]
    public int Runtime { get; set; }

    /// <summary>
    /// Gets or sets the status of the movie.
    /// </summary>
    [Required(ErrorMessage = "The status is required.")]
    [StringLength(50, ErrorMessage = "The status must be at most 50 characters long.")]
    [JsonPropertyName("status")]
    public string Status { get; set; }

    /// <summary>
    /// Gets or sets the tagline of the movie.
    /// </summary>
    [Required(ErrorMessage = "The tagline is required.")]
    [StringLength(255, ErrorMessage = "The tagline must be at most 255 characters long.")]
    [JsonPropertyName("tagline")]
    public string Tagline { get; set; }

    /// <summary>
    /// Gets or sets the average votes for the movie.
    /// </summary>
    [Required(ErrorMessage = "The votes average is required.")]
    [Range(0, 10, ErrorMessage = "The votes average must be between 0 and 10.")]
    [JsonPropertyName("votes_avg")]
    public double VotesAverage { get; set; }

    /// <summary>
    /// Gets or sets the total number of votes for the movie.
    /// </summary>
    [Required(ErrorMessage = "The votes count is required.")]
    [Range(0, int.MaxValue, ErrorMessage = "The votes count must be a positive value.")]
    [JsonPropertyName("votes_count")]
    public int VotesCount { get; set; }

    /// <summary>
    /// Gets or sets the production countries of the movie.
    /// </summary>
    [JsonPropertyName("production_countries")]
    public List<MovieProductionCountry> ProductionCountries { get; set; }

    /// <summary>
    /// Gets or sets the languages of the movie.
    /// </summary>
    [JsonPropertyName("languages")]
    public List<MovieLanguage> Languages { get; set; }

    /// <summary>
    /// Gets or sets the genres of the movie.
    /// </summary>
    [JsonPropertyName("genres")]
    public List<MovieGenre> Genres { get; set; }

    /// <summary>
    /// Gets or sets the keywords associated with the movie.
    /// </summary>
    [JsonPropertyName("keywords")]
    public List<MovieKeyword> Keywords { get; set; }

    /// <summary>
    /// Gets or sets the production companies of the movie.
    /// </summary>
    [JsonPropertyName("companies")]
    public List<MovieCompanie> Companies { get; set; }

    /// <summary>
    /// Gets or sets the cast members of the movie.
    /// </summary>
    [JsonPropertyName("casts")]
    public List<MovieCast> Casts { get; set; }

    /// <summary>
    /// Gets or sets the crew members of the movie.
    /// </summary>
    [JsonPropertyName("crews")]
    public List<MovieCrew> Crews { get; set; }
}
