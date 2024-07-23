using Microsoft.EntityFrameworkCore;
usingMovieMania.Core.Contexts.Entities;

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

        ConfigureCountryEntity(modelBuilder);
        ConfigureDepartmentEntity(modelBuilder);
        ConfigureGenderEntity(modelBuilder);
        ConfigureGenreEntity(modelBuilder);
        ConfigureKeywordEntity(modelBuilder);
        ConfigureLanguageEntity(modelBuilder);
        ConfigureLanguageRoleEntity(modelBuilder);
        ConfigurePersonEntity(modelBuilder);
        ConfigureProductionCompanyEntity(modelBuilder);
        ConfigureProductionCountryEntity(modelBuilder);
        ConfigureMovieEntity(modelBuilder);
        ConfigureMovieCastEntity(modelBuilder);
        ConfigureMovieCompanyEntity(modelBuilder);
        ConfigureMovieCrewEntity(modelBuilder);
        ConfigureMovieGenreEntity(modelBuilder);
        ConfigureMovieImageEntity(modelBuilder);
        ConfigureMovieKeywordEntity(modelBuilder);
        ConfigureMovieLanguageEntity(modelBuilder);
    }

    private static void ConfigureMovieLanguageEntity(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MovieLanguageEntity>()
            .ToTable("movie_languages")
            .HasKey(ml => new { ml.MovieId, ml.LanguageId, ml.LanguageRoleId });

        modelBuilder.Entity<MovieLanguageEntity>()
            .HasOne(ml => ml.Movie)
            .WithMany(m => m.Languages)
            .HasForeignKey(ml => ml.MovieId);

        modelBuilder.Entity<MovieLanguageEntity>()
            .HasOne(ml => ml.Language)
            .WithMany(l => l.MovieLanguages)
            .HasForeignKey(ml => ml.LanguageId);

        modelBuilder.Entity<MovieLanguageEntity>()
            .HasOne(ml => ml.LanguageRole)
            .WithMany(lr => lr.MovieLanguages)
            .HasForeignKey(ml => ml.LanguageRoleId);
    }

    private static void ConfigureMovieKeywordEntity(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MovieKeywordEntity>()
            .ToTable("movie_keywords")
            .HasKey(mk => new { mk.MovieId, mk.KeywordId });

        modelBuilder.Entity<MovieKeywordEntity>()
            .HasOne(mk => mk.Movie)
            .WithMany(m => m.Keywords)
            .HasForeignKey(mk => mk.MovieId);

        modelBuilder.Entity<MovieKeywordEntity>()
            .HasOne(mk => mk.Keyword)
            .WithMany(k => k.MovieKeywords)
            .HasForeignKey(mk => mk.KeywordId);
    }

    private static void ConfigureMovieImageEntity(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MovieImageEntity>()
            .ToTable("movie_images");

        modelBuilder.Entity<MovieImageEntity>()
            .HasOne(mi => mi.Movie)
            .WithMany(m => m.Images)
            .HasForeignKey(mi => mi.MovieId);
    }

    private static void ConfigureMovieGenreEntity(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MovieGenreEntity>()
            .ToTable("movie_genres")
            .HasKey(mg => new { mg.MovieId, mg.GenreId });

        modelBuilder.Entity<MovieGenreEntity>()
            .HasOne(mg => mg.Movie)
            .WithMany(m => m.Genres)
            .HasForeignKey(mg => mg.MovieId);

        modelBuilder.Entity<MovieGenreEntity>()
            .HasOne(mg => mg.Genre)
            .WithMany(g => g.MovieGenres)
            .HasForeignKey(mg => mg.GenreId);
    }

    private static void ConfigureMovieCrewEntity(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MovieCrewEntity>()
            .ToTable("movie_crews")
            .HasKey(mc => new { mc.MovieId, mc.PersonId, mc.DepartmentId });

        modelBuilder.Entity<MovieCrewEntity>()
            .HasOne(mc => mc.Movie)
            .WithMany(m => m.Crews)
            .HasForeignKey(mc => mc.MovieId);

        modelBuilder.Entity<MovieCrewEntity>()
            .HasOne(mc => mc.Person)
            .WithMany(p => p.MovieCrews)
            .HasForeignKey(mc => mc.PersonId);

        modelBuilder.Entity<MovieCrewEntity>()
            .HasOne(mc => mc.Department)
            .WithMany(d => d.MovieCrews)
            .HasForeignKey(mc => mc.DepartmentId);
    }

    private static void ConfigureMovieCompanyEntity(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MovieCompanyEntity>()
            .ToTable("movie_companies")
            .HasKey(mc => new { mc.MovieId, mc.CompanyId });

        modelBuilder.Entity<MovieCompanyEntity>()
            .HasOne(mc => mc.Movie)
            .WithMany(m => m.Companies)
            .HasForeignKey(mc => mc.MovieId);

        modelBuilder.Entity<MovieCompanyEntity>()
            .HasOne(mc => mc.ProductionCompany)
            .WithMany(pc => pc.MovieCompanies)
            .HasForeignKey(mc => mc.CompanyId);
    }

    private static void ConfigureMovieCastEntity(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MovieCastEntity>()
            .ToTable("movie_casts")
            .HasKey(mc => new { mc.MovieId, mc.PersonId, mc.GenderId });

        modelBuilder.Entity<MovieCastEntity>()
            .HasOne(mc => mc.Movie)
            .WithMany(m => m.Casts)
            .HasForeignKey(mc => mc.MovieId);

        modelBuilder.Entity<MovieCastEntity>()
            .HasOne(mc => mc.Person)
            .WithMany(p => p.MovieCasts)
            .HasForeignKey(mc => mc.PersonId);

        modelBuilder.Entity<MovieCastEntity>()
            .HasOne(mc => mc.Gender)
            .WithMany(g => g.MovieCasts)
            .HasForeignKey(mc => mc.GenderId);
    }

    private static void ConfigureProductionCountryEntity(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProductionCountryEntity>()
            .ToTable("production_countries")
            .HasKey(pc => new { pc.MovieId, pc.CountryId });

        modelBuilder.Entity<ProductionCountryEntity>()
            .HasOne(pc => pc.Movie)
            .WithMany(m => m.ProductionCountries)
            .HasForeignKey(pc => pc.MovieId);

        modelBuilder.Entity<ProductionCountryEntity>()
            .HasOne(pc => pc.Country)
            .WithMany(c => c.ProductionCountries)
            .HasForeignKey(pc => pc.CountryId);
    }

    private static void ConfigureProductionCompanyEntity(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProductionCompanyEntity>()
            .ToTable("production_companies");

        modelBuilder.Entity<ProductionCompanyEntity>()
            .HasMany(pc => pc.MovieCompanies)
            .WithOne(mc => mc.ProductionCompany)
            .HasForeignKey(mc => mc.CompanyId);
    }

    private static void ConfigurePersonEntity(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PersonEntity>()
            .ToTable("people");

        modelBuilder.Entity<PersonEntity>()
            .HasMany(p => p.MovieCasts)
            .WithOne(mc => mc.Person)
            .HasForeignKey(mc => mc.PersonId);

        modelBuilder.Entity<PersonEntity>()
            .HasMany(p => p.MovieCrews)
            .WithOne(mc => mc.Person)
            .HasForeignKey(mc => mc.PersonId);
    }

    private static void ConfigureMovieEntity(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MovieEntity>()
            .ToTable("movies");
    }

    private static void ConfigureLanguageRoleEntity(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<LanguageRoleEntity>()
            .ToTable("language_roles");

        modelBuilder.Entity<LanguageRoleEntity>()
            .HasMany(lr => lr.MovieLanguages)
            .WithOne(ml => ml.LanguageRole)
            .HasForeignKey(ml => ml.LanguageRoleId);
    }

    private static void ConfigureLanguageEntity(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<LanguageEntity>()
            .ToTable("languages");

        modelBuilder.Entity<LanguageEntity>()
            .HasMany(l => l.MovieLanguages)
            .WithOne(ml => ml.Language)
            .HasForeignKey(ml => ml.LanguageId);
    }

    private static void ConfigureKeywordEntity(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<KeywordEntity>()
            .ToTable("keywords");

        modelBuilder.Entity<KeywordEntity>()
            .HasMany(k => k.MovieKeywords)
            .WithOne(mk => mk.Keyword)
            .HasForeignKey(mk => mk.KeywordId);
    }

    private static void ConfigureGenreEntity(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<GenreEntity>()
            .ToTable("genres");

        modelBuilder.Entity<GenreEntity>()
            .HasMany(g => g.MovieGenres)
            .WithOne(mg => mg.Genre)
            .HasForeignKey(mg => mg.GenreId);
    }

    private static void ConfigureGenderEntity(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<GenderEntity>()
            .ToTable("genders");

        modelBuilder.Entity<GenderEntity>()
            .HasMany(g => g.MovieCasts)
            .WithOne(mc => mc.Gender)
            .HasForeignKey(mc => mc.GenderId);
    }

    private static void ConfigureDepartmentEntity(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DepartmentEntity>()
            .ToTable("departments");

        modelBuilder.Entity<DepartmentEntity>()
            .HasMany(d => d.MovieCrews)
            .WithOne(mc => mc.Department)
            .HasForeignKey(mc => mc.DepartmentId);
    }

    private static void ConfigureCountryEntity(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CountryEntity>()
            .ToTable("countries");

        modelBuilder.Entity<CountryEntity>()
            .HasMany(c => c.ProductionCountries)
            .WithOne(pc => pc.Country)
            .HasForeignKey(pc => pc.CountryId);
    }
}