﻿using System.ComponentModel.DataAnnotations.Schema;
using MovieMania.Core.Configurations.DTOs;

namespace MovieMania.Core.Contexts.Entities;

public record MovieGenreEntity : EntityBase
{
    [ForeignKey("Movie")]
    [Column("movie_id")]
    public int MovieId { get; set; }

    [ForeignKey("Genre")]
    [Column("genre_id")]
    public int GenreId { get; set; }

    public MovieEntity Movie { get; set; }
    public GenreEntity Genre { get; set; }
}
