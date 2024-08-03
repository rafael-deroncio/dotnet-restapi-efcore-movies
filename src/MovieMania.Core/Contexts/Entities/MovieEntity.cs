using MovieMania.Core.Configurations.DTOs;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieMania.Core.Contexts.Entities;

public record MovieEntity : EntityBase
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
    public string Status { get; set; }

    [StringLength(255)]
    [Column("tagline")]
    public string Tagline { get; set; }

    [Column("votes_avg")]
    public double VotesAverage { get; set; }

    [Column("votes_count")]
    public int VotesCount { get; set; }

    public ICollection<ProductionCountryEntity> ProductionCountries { get; set; }
    public ICollection<MovieCompanyEntity> Companies { get; set; }
    public ICollection<MovieLanguageEntity> Languages { get; set; }
    public ICollection<MovieGenreEntity> Genres { get; set; }
    public ICollection<MovieKeywordEntity> Keywords { get; set; }
    public ICollection<MovieCastEntity> Casts { get; set; }
    public ICollection<MovieCrewEntity> Crews { get; set; }
    public ICollection<MovieImageEntity> Images { get; set; }

}
