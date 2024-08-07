using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieMania.Core.Contexts.Entities;

namespace MovieMania.Core.Contexts.Configurations;

public class MovieLanguageEntityConfiguration : IEntityTypeConfiguration<MovieLanguageEntity>
{
    public void Configure(EntityTypeBuilder<MovieLanguageEntity> builder)
    {
        builder.ToTable("movie_languages")
            .HasKey(ml => new { ml.MovieId, ml.LanguageId, ml.LanguageRoleId });

        builder.HasOne(ml => ml.Movie)
            .WithMany(m => m.Languages)
            .HasForeignKey(ml => ml.MovieId);

        builder.HasOne(ml => ml.Language)
            .WithMany(l => l.MovieLanguages)
            .HasForeignKey(ml => ml.LanguageId);

        builder.HasOne(ml => ml.LanguageRole)
            .WithMany(lr => lr.MovieLanguages)
            .HasForeignKey(ml => ml.LanguageRoleId);
    }
}

