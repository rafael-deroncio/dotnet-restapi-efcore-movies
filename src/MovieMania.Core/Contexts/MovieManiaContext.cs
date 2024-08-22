using Microsoft.EntityFrameworkCore;
using MovieMania.Core.Contexts.Configurations;
using MovieMania.Core.Contexts.Entities;

namespace MovieMania.Core.Contexts;

public class MovieManiaContext(DbContextOptions<MovieManiaContext> options) : DbContext(options)
{
    public virtual DbSet<CountryEntity> Countries { get; set; }
    public virtual DbSet<DepartmentEntity> Departments { get; set; }
    public virtual DbSet<GenderEntity> Genders { get; set; }
    public virtual DbSet<GenreEntity> Genres { get; set; }
    public virtual DbSet<KeywordEntity> Keywords { get; set; }
    public virtual DbSet<LanguageEntity> Languages { get; set; }
    public virtual DbSet<LanguageRoleEntity> LanguageRoles { get; set; }
    public virtual DbSet<MovieCastEntity> MovieCasts { get; set; }
    public virtual DbSet<MovieCompanyEntity> MovieCompanies { get; set; }
    public virtual DbSet<MovieCrewEntity> MovieCrews { get; set; }
    public virtual DbSet<MovieGenreEntity> MovieGenres { get; set; }
    public virtual DbSet<MovieImageEntity> MovieImages { get; set; }
    public virtual DbSet<MovieKeywordEntity> MovieKeywords { get; set; }
    public virtual DbSet<MovieLanguageEntity> MovieLanguages { get; set; }
    public virtual DbSet<MovieEntity> Movies { get; set; }
    public virtual DbSet<PersonEntity> Persons { get; set; }
    public virtual DbSet<ProductionCompanyEntity> ProductionCompanies { get; set; }
    public virtual DbSet<ProductionCountryEntity> ProductionCountries { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.ApplyConfiguration(new ProductionCountryEntityConfiguration());
        modelBuilder.ApplyConfiguration(new MovieLanguageEntityConfiguration());
        modelBuilder.ApplyConfiguration(new MovieGenreEntityConfiguration());
        modelBuilder.ApplyConfiguration(new MovieKeywordEntityConfiguration());
        modelBuilder.ApplyConfiguration(new MovieCompanyEntityConfiguration());
        modelBuilder.ApplyConfiguration(new MovieCastEntityConfiguration());
        modelBuilder.ApplyConfiguration(new MovieCrewEntityConfiguration());
    }
}