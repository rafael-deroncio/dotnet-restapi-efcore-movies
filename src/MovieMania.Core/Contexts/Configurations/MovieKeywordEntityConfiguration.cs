using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieMania.Core.Contexts.Entities;

namespace MovieMania.Core.Contexts.Configurations;

public class MovieKeywordEntityConfiguration : IEntityTypeConfiguration<MovieKeywordEntity>
{
    public void Configure(EntityTypeBuilder<MovieKeywordEntity> builder)
    {
        builder.ToTable("movie_keywords")
            .HasKey(mk => new { mk.MovieId, mk.KeywordId });

        builder.HasOne(mk => mk.Movie)
            .WithMany(m => m.Keywords)
            .HasForeignKey(mk => mk.MovieId);

        builder.HasOne(mk => mk.Keyword)
            .WithMany(k => k.MovieKeywords)
            .HasForeignKey(mk => mk.KeywordId);
    }
}