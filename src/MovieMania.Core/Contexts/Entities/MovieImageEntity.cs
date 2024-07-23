using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MovieMania.Core.Configurations.DTOs;

namespaceMovieMania.Core.Contexts.Entities;

public record MovieImageEntity : EntityBase
{
    [Key]
    [Column("image_id")]
    public int ImageId { get; set; }

    [ForeignKey("Movie")]
    [Column("movie_id")]
    public int MovieId { get; set; }

    [Required]
    [StringLength(255)]
    [Column("image_url")]
    public string ImageUrl { get; set; }

    public MovieEntity Movie { get; set; }
}

