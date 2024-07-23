using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieMania.Core.Contexts.Entities;

namespace MovieMania.Core.Contexts.Configurations;

public class LanguageEntityConfiguration : IEntityTypeConfiguration<LanguageEntity>
{
    public void Configure(EntityTypeBuilder<LanguageEntity> builder)
    {
        builder.ToTable("languages");

        builder.HasMany(l => l.MovieLanguages)
            .WithOne(ml => ml.Language)
            .HasForeignKey(ml => ml.LanguageId);
    }
}