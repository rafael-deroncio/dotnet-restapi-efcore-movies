using MovieMania.Core.Configurations.DTOs;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieMania.Core.Models;

public record MovieModel : ContextBaseDTO
{
    [Key]
    [Column("movie_id")]
    public int MovieId { get; set; }

    [Required]
    [StringLength(255)]
    [Column("title")]
    public string Title { get; set; }

    [Column("budget")]
    public decimal Budget { get; set; }

    [StringLength(255)]
    [Column("homepage")]
    public string Homepage { get; set; }

    [Column("overview")]
    public string Overview { get; set; }

    [Column("popularity")]
    public double Popularity { get; set; }

    [Column("release_date")]
    public DateTime ReleaseDate { get; set; }

    [Column("revenue")]
    public double Revenue { get; set; }

    [Column("runtime")]
    public int Runtime { get; set; }

    [StringLength(50)]
    [Column("movie_status")]
    public string MovieStatus { get; set; }

    [StringLength(255)]
    [Column("tagline")]
    public string Tagline { get; set; }

    [Column("votes_avg")]
    public double VotesAverage { get; set; }

    [Column("votes_count")]
    public int VotesCount { get; set; }

    public ICollection<ProductionCountryModel> ProductionCountries { get; set; }
    public ICollection<MovieCompanyModel> Companies { get; set; }
    public ICollection<MovieLanguageModel> Languages { get; set; }
    public ICollection<MovieGenreModel> Genres { get; set; }
    public ICollection<MovieKeywordModel> Keywords { get; set; }
    public ICollection<MovieCastModel> Casts { get; set; }
    public ICollection<MovieCrewModel> Crews { get; set; }
    public ICollection<MovieImageModel> Images { get; set; }

}
