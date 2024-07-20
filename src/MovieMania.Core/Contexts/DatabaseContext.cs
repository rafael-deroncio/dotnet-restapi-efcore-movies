using Microsoft.EntityFrameworkCore;
using MovieMania.Core.Models;

namespace MovieMania.Core.Contexts;

public class MovieManiaContext(DbContextOptions<MovieManiaContext> options) : DbContext(options)
{
    public DbSet<CountryModel> Countries { get; set; }
    public DbSet<DepartmentModel> Departments { get; set; }
    public DbSet<GenderModel> Genders { get; set; }
    public DbSet<GenreModel> Genres { get; set; }
    public DbSet<KeywordModel> Keywords { get; set; }
    public DbSet<LanguageModel> Languages { get; set; }
    public DbSet<LanguageRoleModel> LanguageRoles { get; set; }
    public DbSet<MovieCastModel> MovieCasts { get; set; }
    public DbSet<MovieCompanyModel> MovieCompanies { get; set; }
    public DbSet<MovieCrewModel> MovieCrews { get; set; }
    public DbSet<MovieGenreModel> MovieGenres { get; set; }
    public DbSet<MovieImageModel> MovieImages { get; set; }
    public DbSet<MovieKeywordModel> MovieKeywords { get; set; }
    public DbSet<MovieLanguageModel> MovieLanguages { get; set; }
    public DbSet<MovieModel> Movies { get; set; }
    public DbSet<PersonModel> People { get; set; }
    public DbSet<ProductionCompanyModel> ProductionCompanies { get; set; }
    public DbSet<ProductionCountryModel> ProductionCountries { get; set; }

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
        modelBuilder.Entity<MovieLanguageModel>()
            .ToTable("movie_languages")
            .HasKey(ml => new { ml.MovieId, ml.LanguageId, ml.LanguageRoleId });

        modelBuilder.Entity<MovieLanguageModel>()
            .HasOne(ml => ml.Movie)
            .WithMany(m => m.Languages)
            .HasForeignKey(ml => ml.MovieId);

        modelBuilder.Entity<MovieLanguageModel>()
            .HasOne(ml => ml.Language)
            .WithMany(l => l.MovieLanguages)
            .HasForeignKey(ml => ml.LanguageId);

        modelBuilder.Entity<MovieLanguageModel>()
            .HasOne(ml => ml.LanguageRole)
            .WithMany(lr => lr.MovieLanguages)
            .HasForeignKey(ml => ml.LanguageRoleId);
    }

    private static void ConfigureMovieKeywordEntity(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MovieKeywordModel>()
            .ToTable("movie_keywords")
            .HasKey(mk => new { mk.MovieId, mk.KeywordId });

        modelBuilder.Entity<MovieKeywordModel>()
            .HasOne(mk => mk.Movie)
            .WithMany(m => m.Keywords)
            .HasForeignKey(mk => mk.MovieId);

        modelBuilder.Entity<MovieKeywordModel>()
            .HasOne(mk => mk.Keyword)
            .WithMany(k => k.MovieKeywords)
            .HasForeignKey(mk => mk.KeywordId);
    }

    private static void ConfigureMovieImageEntity(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MovieImageModel>()
            .ToTable("movie_images");

        modelBuilder.Entity<MovieImageModel>()
            .HasOne(mi => mi.Movie)
            .WithMany(m => m.Images)
            .HasForeignKey(mi => mi.MovieId);
    }

    private static void ConfigureMovieGenreEntity(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MovieGenreModel>()
            .ToTable("movie_genres")
            .HasKey(mg => new { mg.MovieId, mg.GenreId });

        modelBuilder.Entity<MovieGenreModel>()
            .HasOne(mg => mg.Movie)
            .WithMany(m => m.Genres)
            .HasForeignKey(mg => mg.MovieId);

        modelBuilder.Entity<MovieGenreModel>()
            .HasOne(mg => mg.Genre)
            .WithMany(g => g.MovieGenres)
            .HasForeignKey(mg => mg.GenreId);
    }

    private static void ConfigureMovieCrewEntity(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MovieCrewModel>()
            .ToTable("movie_crews")
            .HasKey(mc => new { mc.MovieId, mc.PersonId, mc.DepartmentId });

        modelBuilder.Entity<MovieCrewModel>()
            .HasOne(mc => mc.Movie)
            .WithMany(m => m.Crews)
            .HasForeignKey(mc => mc.MovieId);

        modelBuilder.Entity<MovieCrewModel>()
            .HasOne(mc => mc.Person)
            .WithMany(p => p.MovieCrews)
            .HasForeignKey(mc => mc.PersonId);

        modelBuilder.Entity<MovieCrewModel>()
            .HasOne(mc => mc.Department)
            .WithMany(d => d.MovieCrews)
            .HasForeignKey(mc => mc.DepartmentId);
    }

    private static void ConfigureMovieCompanyEntity(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MovieCompanyModel>()
            .ToTable("movie_companies")
            .HasKey(mc => new { mc.MovieId, mc.CompanyId });

        modelBuilder.Entity<MovieCompanyModel>()
            .HasOne(mc => mc.Movie)
            .WithMany(m => m.Companies)
            .HasForeignKey(mc => mc.MovieId);

        modelBuilder.Entity<MovieCompanyModel>()
            .HasOne(mc => mc.ProductionCompany)
            .WithMany(pc => pc.MovieCompanies)
            .HasForeignKey(mc => mc.CompanyId);
    }

    private static void ConfigureMovieCastEntity(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MovieCastModel>()
            .ToTable("movie_casts")
            .HasKey(mc => new { mc.MovieId, mc.PersonId, mc.GenderId });

        modelBuilder.Entity<MovieCastModel>()
            .HasOne(mc => mc.Movie)
            .WithMany(m => m.Casts)
            .HasForeignKey(mc => mc.MovieId);

        modelBuilder.Entity<MovieCastModel>()
            .HasOne(mc => mc.Person)
            .WithMany(p => p.MovieCasts)
            .HasForeignKey(mc => mc.PersonId);

        modelBuilder.Entity<MovieCastModel>()
            .HasOne(mc => mc.Gender)
            .WithMany(g => g.MovieCasts)
            .HasForeignKey(mc => mc.GenderId);
    }

    private static void ConfigureProductionCountryEntity(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProductionCountryModel>()
            .ToTable("production_countries")
            .HasKey(pc => new { pc.MovieId, pc.CountryId });

        modelBuilder.Entity<ProductionCountryModel>()
            .HasOne(pc => pc.Movie)
            .WithMany(m => m.ProductionCountries)
            .HasForeignKey(pc => pc.MovieId);

        modelBuilder.Entity<ProductionCountryModel>()
            .HasOne(pc => pc.Country)
            .WithMany(c => c.ProductionCountries)
            .HasForeignKey(pc => pc.CountryId);
    }

    private static void ConfigureProductionCompanyEntity(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProductionCompanyModel>()
            .ToTable("production_companies");

        modelBuilder.Entity<ProductionCompanyModel>()
            .HasMany(pc => pc.MovieCompanies)
            .WithOne(mc => mc.ProductionCompany)
            .HasForeignKey(mc => mc.CompanyId);
    }

    private static void ConfigurePersonEntity(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PersonModel>()
            .ToTable("people");

        modelBuilder.Entity<PersonModel>()
            .HasMany(p => p.MovieCasts)
            .WithOne(mc => mc.Person)
            .HasForeignKey(mc => mc.PersonId);

        modelBuilder.Entity<PersonModel>()
            .HasMany(p => p.MovieCrews)
            .WithOne(mc => mc.Person)
            .HasForeignKey(mc => mc.PersonId);
    }

    private static void ConfigureMovieEntity(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MovieModel>()
            .ToTable("movies");
    }

    private static void ConfigureLanguageRoleEntity(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<LanguageRoleModel>()
            .ToTable("language_roles");

        modelBuilder.Entity<LanguageRoleModel>()
            .HasMany(lr => lr.MovieLanguages)
            .WithOne(ml => ml.LanguageRole)
            .HasForeignKey(ml => ml.LanguageRoleId);
    }

    private static void ConfigureLanguageEntity(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<LanguageModel>()
            .ToTable("languages");

        modelBuilder.Entity<LanguageModel>()
            .HasMany(l => l.MovieLanguages)
            .WithOne(ml => ml.Language)
            .HasForeignKey(ml => ml.LanguageId);
    }

    private static void ConfigureKeywordEntity(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<KeywordModel>()
            .ToTable("keywords");

        modelBuilder.Entity<KeywordModel>()
            .HasMany(k => k.MovieKeywords)
            .WithOne(mk => mk.Keyword)
            .HasForeignKey(mk => mk.KeywordId);
    }

    private static void ConfigureGenreEntity(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<GenreModel>()
            .ToTable("genres");

        modelBuilder.Entity<GenreModel>()
            .HasMany(g => g.MovieGenres)
            .WithOne(mg => mg.Genre)
            .HasForeignKey(mg => mg.GenreId);
    }

    private static void ConfigureGenderEntity(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<GenderModel>()
            .ToTable("genders");

        modelBuilder.Entity<GenderModel>()
            .HasMany(g => g.MovieCasts)
            .WithOne(mc => mc.Gender)
            .HasForeignKey(mc => mc.GenderId);
    }

    private static void ConfigureDepartmentEntity(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DepartmentModel>()
            .ToTable("departments");

        modelBuilder.Entity<DepartmentModel>()
            .HasMany(d => d.MovieCrews)
            .WithOne(mc => mc.Department)
            .HasForeignKey(mc => mc.DepartmentId);
    }

    private static void ConfigureCountryEntity(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CountryModel>()
            .ToTable("countries");

        modelBuilder.Entity<CountryModel>()
            .HasMany(c => c.ProductionCountries)
            .WithOne(pc => pc.Country)
            .HasForeignKey(pc => pc.CountryId);
    }
}