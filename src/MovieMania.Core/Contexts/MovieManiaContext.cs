using Microsoft.EntityFrameworkCore;
using MovieMania.Core.Contexts.Entities;

namespace MovieMania.Core.Contexts;

public class MovieManiaContext(DbContextOptions<MovieManiaContext> options) : DbContext(options)
{
    public DbSet<CountryEntity> Countries { get; set; }
    public DbSet<DepartmentEntity> Departments { get; set; }
    public DbSet<GenderEntity> Genders { get; set; }
    public DbSet<GenreEntity> Genres { get; set; }
    public DbSet<KeywordEntity> Keywords { get; set; }
    public DbSet<LanguageEntity> Languages { get; set; }
    public DbSet<LanguageRoleEntity> LanguageRoles { get; set; }
    public DbSet<MovieCastEntity> MovieCasts { get; set; }
    public DbSet<MovieCompanyEntity> MovieCompanies { get; set; }
    public DbSet<MovieCrewEntity> MovieCrews { get; set; }
    public DbSet<MovieGenreEntity> MovieGenres { get; set; }
    public DbSet<MovieImageEntity> MovieImages { get; set; }
    public DbSet<MovieKeywordEntity> MovieKeywords { get; set; }
    public DbSet<MovieLanguageEntity> MovieLanguages { get; set; }
    public DbSet<MovieEntity> Movies { get; set; }
    public DbSet<PersonEntity> Persons { get; set; }
    public DbSet<ProductionCompanyEntity> ProductionCompanies { get; set; }
    public DbSet<ProductionCountryEntity> ProductionCountries { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MovieManiaContext).Assembly);
    }
}