using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieMania.Core.Contexts.Entities;

namespace MovieMania.Core.Contexts.Configurations;

public class GenderEntityConfiguration : IEntityTypeConfiguration<GenderEntity>
{
    public void Configure(EntityTypeBuilder<GenderEntity> builder)
    {
        builder.ToTable("genders");
        
        builder.HasMany(g => g.MovieCasts)
            .WithOne(mc => mc.Gender)
            .HasForeignKey(mc => mc.GenderId);
    }
}