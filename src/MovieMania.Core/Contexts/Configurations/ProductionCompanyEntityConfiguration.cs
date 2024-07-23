using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieMania.Core.Contexts.Entities;

namespace MovieMania.Core.Contexts.Configurations;

public class ProductionCompanyEntityConfiguration : IEntityTypeConfiguration<ProductionCompanyEntity>
{
    public void Configure(EntityTypeBuilder<ProductionCompanyEntity> builder)
    {
        builder.ToTable("production_companies");

        builder.HasMany(pc => pc.MovieCompanies)
            .WithOne(mc => mc.ProductionCompany)
            .HasForeignKey(mc => mc.CompanyId);
    }
}