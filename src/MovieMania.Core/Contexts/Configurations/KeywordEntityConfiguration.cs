using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieMania.Core.Contexts.Entities;

namespace MovieMania.Core.Contexts.Configurations;

public class KeywordEntityConfiguration : IEntityTypeConfiguration<KeywordEntity>
{
    public void Configure(EntityTypeBuilder<KeywordEntity> builder)
    {
        builder.ToTable("keywords");

        builder.HasMany(k => k.MovieKeywords)
            .WithOne(mk => mk.Keyword)
            .HasForeignKey(mk => mk.KeywordId);
    }
}