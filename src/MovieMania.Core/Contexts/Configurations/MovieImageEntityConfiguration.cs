using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieMania.Core.Contexts.Entities;

namespace MovieMania.Core.Contexts.Configurations;

public class MovieImageEntityConfiguration : IEntityTypeConfiguration<MovieImageEntity>
{
    public void Configure(EntityTypeBuilder<MovieImageEntity> builder)
    {
        builder.ToTable("movie_images");

        builder.HasOne(mi => mi.Movie)
            .WithMany(m => m.Images)
            .HasForeignKey(mi => mi.MovieId);
    }
}