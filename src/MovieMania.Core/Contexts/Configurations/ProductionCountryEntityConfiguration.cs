using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieMania.Core.Contexts.Entities;

namespace MovieMania.Core.Contexts.Configurations;

public class ProductionCountryEntityConfiguration : IEntityTypeConfiguration<ProductionCountryEntity>
{
    public void Configure(EntityTypeBuilder<ProductionCountryEntity> builder)
    {
        builder.ToTable("production_countries")
            .HasKey(pc => new { pc.MovieId, pc.CountryId });

        builder.HasOne(pc => pc.Movie)
            .WithMany(m => m.ProductionCountries)
            .HasForeignKey(pc => pc.MovieId);

        builder.HasOne(pc => pc.Country)
            .WithMany(c => c.ProductionCountries)
            .HasForeignKey(pc => pc.CountryId);
    }
}
