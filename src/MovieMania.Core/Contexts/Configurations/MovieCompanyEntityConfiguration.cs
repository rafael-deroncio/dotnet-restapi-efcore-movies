using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieMania.Core.Contexts.Entities;

namespace MovieMania.Core.Contexts.Configurations;

public class MovieCompanyEntityConfiguration : IEntityTypeConfiguration<MovieCompanyEntity>
{
    public void Configure(EntityTypeBuilder<MovieCompanyEntity> builder)
    {
        builder.ToTable("movie_companies")
            .HasKey(mc => new { mc.MovieId, mc.CompanyId });

        builder.HasOne(mc => mc.Movie)
            .WithMany(m => m.Companies)
            .HasForeignKey(mc => mc.MovieId);

        builder.HasOne(mc => mc.ProductionCompany)
            .WithMany(pc => pc.MovieCompanies)
            .HasForeignKey(mc => mc.CompanyId);
    }
}
