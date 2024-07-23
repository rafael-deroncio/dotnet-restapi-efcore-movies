using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieMania.Core.Contexts.Entities;

namespace MovieMania.Core.Contexts.Configurations;

public class LanguageRoleEntityConfiguration : IEntityTypeConfiguration<LanguageRoleEntity>
{
    public void Configure(EntityTypeBuilder<LanguageRoleEntity> builder)
    {
        builder.ToTable("language_roles");

        builder.HasMany(lr => lr.MovieLanguages)
            .WithOne(ml => ml.LanguageRole)
            .HasForeignKey(ml => ml.LanguageRoleId);
    }
}