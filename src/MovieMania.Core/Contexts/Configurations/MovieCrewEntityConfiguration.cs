using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieMania.Core.Contexts.Entities;

namespace MovieMania.Core.Contexts.Configurations;

public class MovieCrewEntityConfiguration : IEntityTypeConfiguration<MovieCrewEntity>
{
    public void Configure(EntityTypeBuilder<MovieCrewEntity> builder)
    {
        builder.ToTable("movie_crews")
            .HasKey(mc => new { mc.MovieId, mc.PersonId, mc.DepartmentId });

        builder.HasOne(mc => mc.Movie)
            .WithMany(m => m.Crews)
            .HasForeignKey(mc => mc.MovieId);

        builder.HasOne(mc => mc.Person)
            .WithMany(p => p.MovieCrews)
            .HasForeignKey(mc => mc.PersonId);

        builder.HasOne(mc => mc.Department)
            .WithMany(d => d.MovieCrews)
            .HasForeignKey(mc => mc.DepartmentId);
    }
}