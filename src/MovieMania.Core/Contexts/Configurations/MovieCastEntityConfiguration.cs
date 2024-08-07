using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieMania.Core.Contexts.Entities;

namespace MovieMania.Core.Contexts.Configurations;

public class MovieCastEntityConfiguration : IEntityTypeConfiguration<MovieCastEntity>
{
    public void Configure(EntityTypeBuilder<MovieCastEntity> builder)
    {
        builder.ToTable("movie_casts")
            .HasKey(mc => new { mc.MovieId, mc.PersonId, mc.GenderId });

        builder.HasOne(mc => mc.Movie)
            .WithMany(m => m.Casts)
            .HasForeignKey(mc => mc.MovieId);

        builder.HasOne(mc => mc.Gender)
            .WithMany(g => g.MovieCasts)
            .HasForeignKey(mc => mc.GenderId);

        builder.HasOne(mc => mc.Person)
            .WithMany(p => p.MovieCasts)
            .HasForeignKey(mc => mc.PersonId);
    }
}
