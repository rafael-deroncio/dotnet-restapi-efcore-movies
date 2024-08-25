using AutoFixture;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MovieMania.Core.Configurations.Mapper;
using MovieMania.Core.Configurations.Mapper.Interfaces;
using MovieMania.Core.Contexts;
using MovieMania.Core.Contexts.Entities;
using MovieMania.Core.Exceptions;
using MovieMania.Core.Repositories.Interfaces;
using MovieMania.Core.Requests;
using MovieMania.Core.Services;
using MovieMania.Core.Services.Interfaces;
using MovieMania.Domain.Requests;
using MovieMania.Domain.Responses;
using NSubstitute;
using NSubstitute.Core;
using NSubstitute.Core.Arguments;
using NSubstitute.ExceptionExtensions;

namespace MovieMania.Test.Fixtures;

public class MovieServiceFixture
{
    private readonly Fixture _fixture;
    private readonly ILogger<IMovieService> _logger;
    private readonly MovieManiaContext _context;
    private readonly DbSet<MovieEntity> _entity;
    private readonly IPaginationService _paginationService;
    private readonly IObjectConverter _mapper;
    private readonly ICountryService _countryService;
    private readonly IDepartmentService _departmentService;
    private readonly IGenderService _genderService;
    private readonly IGenreService _genreService;
    private readonly IKeywordService _keywordService;
    private readonly ILanguageService _languageService;
    private readonly IPersonService _personService;
    private readonly IProductionCompanyService _productionCompanyService;

    public MovieServiceFixture()
    {
        _fixture = GetFixture();
        _context = GetContext();
        _logger = Substitute.For<ILogger<IMovieService>>();
        _entity = Substitute.For<DbSet<MovieEntity>>();
        _paginationService = Substitute.For<IPaginationService>();
        _mapper = new ObjectConverter();
        _countryService = Substitute.For<ICountryService>();
        _departmentService = Substitute.For<IDepartmentService>();
        _genderService = Substitute.For<IGenderService>();
        _genreService = Substitute.For<IGenreService>();
        _keywordService = Substitute.For<IKeywordService>();
        _languageService = Substitute.For<ILanguageService>();
        _personService = Substitute.For<IPersonService>();
        _productionCompanyService = Substitute.For<IProductionCompanyService>();
    }

    public MovieService Instance()
        => new(
            _logger,
            _context,
            _paginationService,
            _mapper,
            _countryService,
            _departmentService,
            _genderService,
            _genreService,
            _keywordService,
            _languageService,
            _personService,
            _productionCompanyService
        );

    #region Calls
    public IEnumerable<ICall> MovieManiaContextCalls() => _context.ReceivedCalls();
    public IEnumerable<ICall> DbSetMovieEntityCalls() => _entity.ReceivedCalls();
    public IEnumerable<ICall> LoggerCalls() => _logger.ReceivedCalls();
    public IEnumerable<ICall> PaginationServiceCalls() => _paginationService.ReceivedCalls();
    public IEnumerable<ICall> CountryServiceCalls() => _countryService.ReceivedCalls();
    public IEnumerable<ICall> DepartmentServiceCalls() => _departmentService.ReceivedCalls();
    public IEnumerable<ICall> GenderServiceCalls() => _genderService.ReceivedCalls();
    public IEnumerable<ICall> GenreServiceCalls() => _genreService.ReceivedCalls();
    public IEnumerable<ICall> KeywordServiceCalls() => _keywordService.ReceivedCalls();
    public IEnumerable<ICall> LanguageServiceCalls() => _languageService.ReceivedCalls();
    public IEnumerable<ICall> PersonServiceCalls() => _personService.ReceivedCalls();
    public IEnumerable<ICall> ProductionCompanyServiceCalls() => _productionCompanyService.ReceivedCalls();
    #endregion

    #region Fixtures
    public MovieServiceFixture WithMovieEntity(int id = 0, string title = null)
    {
        MovieEntity entity = _fixture.Create<MovieEntity>();
        entity.MovieId = id;
        entity.Title = string.IsNullOrEmpty(title) ? entity.Title : title;

        _context.Movies = ConfigureEntity(entity);
        return this;
    }

    public MovieServiceFixture WithCountryEntity(int id = 0)
    {
        CountryEntity entity = _fixture.Create<CountryEntity>();
        entity.CountryId = id;

        _context.Countries = ConfigureEntity(entity); ;
        return this;
    }

    public MovieServiceFixture WithLanguageRoleEntity(int id = 0)
    {
        LanguageRoleEntity entity = _fixture.Create<LanguageRoleEntity>();
        entity.LanguageRoleId = id;

        _context.LanguageRoles = ConfigureEntity(entity); ;
        return this;
    }

    public MovieServiceFixture WithLanguageEntity(int id = 0)
    {
        LanguageEntity entity = _fixture.Create<LanguageEntity>();
        entity.LanguageId = id;

        _context.Languages = ConfigureEntity(entity); ;
        return this;
    }

    public MovieServiceFixture WithGenreEntity(int id = 0)
    {
        GenreEntity entity = _fixture.Create<GenreEntity>();
        entity.GenreId = id;

        _context.Genres = ConfigureEntity(entity); ;
        return this;
    }

    public MovieServiceFixture WithKeywordEntity(int id = 0)
    {
        KeywordEntity entity = _fixture.Create<KeywordEntity>();
        entity.KeywordId = id;

        _context.Keywords = ConfigureEntity(entity); ;
        return this;
    }

    public MovieServiceFixture WithProductionCompanyEntity(int id = 0)
    {
        ProductionCompanyEntity entity = _fixture.Create<ProductionCompanyEntity>();
        entity.CompanyId = id;

        _context.ProductionCompanies = ConfigureEntity(entity); ;
        return this;
    }

    public MovieServiceFixture WithGenderEntity(int id = 0)
    {
        GenderEntity entity = _fixture.Create<GenderEntity>();
        entity.GenderId = id;

        _context.Genders = ConfigureEntity(entity); ;
        return this;
    }

    public MovieServiceFixture WithPersonEntity(int id = 0)
    {
        PersonEntity entity = _fixture.Create<PersonEntity>();
        entity.PersonId = id;

        _context.Persons = ConfigureEntity(entity);
        return this;
    }

    public MovieServiceFixture WithDepartmentEntity(int id = 0)
    {
        DepartmentEntity entity = _fixture.Create<DepartmentEntity>();
        entity.DepartmentId = id;

        _context.Departments = ConfigureEntity(entity);
        return this;
    }

    public MovieServiceFixture WithGetCountryById(bool returnsNull = false)
    {
        int id = Arg.Any<int>();

        if (returnsNull)
        {
            EntityNotFoundException exception = new("Country Not Found", $"Country with id {id} not exists");
            _countryService.GetCountryById(id).Throws(exception);
        }
        else
        {
            CountryResponse response = _fixture.Create<CountryResponse>();
            _countryService.GetCountryById(id).ReturnsForAnyArgs(response);
        }

        return this;
    }

    public MovieServiceFixture WithGetLanguageById(bool returnsNull = false)
    {
        int id = Arg.Any<int>();

        if (returnsNull)
        {
            EntityNotFoundException exception = new("Language Not Found", $"Language with id {id} not exists");
            _languageService.GetLanguageById(id).Throws(exception);
        }
        else
        {

            LanguageResponse response = _fixture.Create<LanguageResponse>();
            _languageService.GetLanguageById(id).Returns(response);
        }

        return this;
    }

    public MovieServiceFixture WithGetLanguageRoleById(bool returnsNull = false)
    {
        int id = Arg.Any<int>();

        if (returnsNull)
        {
            EntityNotFoundException exception = new("Language Role Not Found", $"Language Role with id {id} not exists");
            _languageService.GetLanguageRoleById(id).Throws(exception);
        }
        else
        {
            LanguageRoleResponse response = _fixture.Create<LanguageRoleResponse>();
            _languageService.GetLanguageRoleById(id).Returns(response);
        }

        return this;
    }

    public MovieServiceFixture WithGetGenreById(bool returnsNull = false)
    {
        int id = Arg.Any<int>();

        if (returnsNull)
        {
            EntityNotFoundException exception = new("Genre Not Found", $"Genre with id {id} not exists");
            _genreService.GetGenreById(id).Throws(exception);
        }
        else
        {
            GenreResponse response = _fixture.Create<GenreResponse>();
            _genreService.GetGenreById(id).Returns(response);
        }

        return this;
    }

    public MovieServiceFixture WithGetKeywordById(bool returnsNull = false)
    {
        int id = Arg.Any<int>();

        if (returnsNull)
        {
            EntityNotFoundException exception = new("Keyword Not Found", $"Keyword with id {id} not exists");
            _keywordService.GetKeywordById(id).Throws(exception);
        }
        else
        {
            KeywordResponse response = _fixture.Create<KeywordResponse>();
            _keywordService.GetKeywordById(id).Returns(response);
        }

        return this;
    }

    public MovieServiceFixture WithGetProductionCompanyById(bool returnsNull = false)
    {
        int id = Arg.Any<int>();

        if (returnsNull)
        {
            EntityNotFoundException exception = new("Production Company Not Found", $"Production Company with id {id} not exists");
            _productionCompanyService.GetProductionCompanyById(id).Throws(exception);
        }
        else
        {
            ProductionCompanyResponse response = _fixture.Create<ProductionCompanyResponse>();
            _productionCompanyService.GetProductionCompanyById(id).Returns(response);
        }

        return this;
    }

    public MovieServiceFixture WithGetGenderById(bool returnsNull = false)
    {
        int id = Arg.Any<int>();

        if (returnsNull)
        {
            EntityNotFoundException exception = new("Gender Not Found", $"Gender with id {id} not exists");
            _genderService.GetGenderById(id).Throws(exception);
        }
        else
        {
            GenderResponse response = _fixture.Create<GenderResponse>();
            _genderService.GetGenderById(id).Returns(response);
        }

        return this;
    }

    public MovieServiceFixture WithGetPersonById(bool returnsNull = false)
    {
        int id = Arg.Any<int>();

        if (returnsNull)
        {
            EntityNotFoundException exception = new("Person Not Found", $"Person with id {id} not exists");
            _personService.GetPersonById(id).Throws(exception);
        }
        else
        {
            PersonResponse response = _fixture.Create<PersonResponse>();
            _personService.GetPersonById(id).Returns(response);
        }

        return this;
    }

    public MovieServiceFixture WithGetDepartmentById(bool returnsNull = false)
    {
        int id = Arg.Any<int>();

        if (returnsNull)
        {
            EntityNotFoundException exception = new("Department Not Found", $"Department with id {id} not exists");
            _departmentService.GetDepartmentById(id).Throws(exception);
        }
        else
        {
            DepartmentResponse response = _fixture.Create<DepartmentResponse>();
            _departmentService.GetDepartmentById(id).Returns(response);
        }
        return this;
    }
    #endregion

    #region Mocks
    public MovieRequest MovieRequestMock() => _fixture.Create<MovieRequest>();
    public PaginationRequest PaginationRequestMock() => _fixture.Create<PaginationRequest>();
    public MovieFilterRequest MovieFilterRequestMock(bool full = false)
    {
        if (full)
            _fixture.Customize<MovieFilterRequest>(composer =>
            {
                return composer
                    .With(x => x.AddProductionCountries, true)
                    .With(x => x.AddCompanies, true)
                    .With(x => x.AddLanguages, true)
                    .With(x => x.AddGenres, true)
                    .With(x => x.AddKeywords, true)
                    .With(x => x.AddCasts, true)
                    .With(x => x.AddCrews, true)
                    .With(x => x.AddImages, true);
            });
        else
            _fixture.Customize<MovieFilterRequest>(composer =>
            {
                return composer
                    .With(x => x.AddProductionCountries, false)
                    .With(x => x.AddCompanies, false)
                    .With(x => x.AddLanguages, false)
                    .With(x => x.AddGenres, false)
                    .With(x => x.AddKeywords, false)
                    .With(x => x.AddCasts, false)
                    .With(x => x.AddCrews, false)
                    .With(x => x.AddImages, false);
            });

        return _fixture.Create<MovieFilterRequest>();
    }

    #endregion

    #region Configure
    private static Fixture GetFixture()
    {
        Fixture fixture = new();

        fixture.Behaviors
            .OfType<ThrowingRecursionBehavior>()
            .ToList()
            .ForEach(b => fixture.Behaviors.Remove(b));

        fixture.Behaviors.Add(new OmitOnRecursionBehavior());

        return fixture;
    }

    private static MovieManiaContext GetContext()
    {
        return Substitute.For<MovieManiaContext>(new DbContextOptionsBuilder<MovieManiaContext>()
                    .UseInMemoryDatabase(databaseName: "MovieManiaTest")
                    .Options);
    }

    private static DbSet<MovieEntity> ConfigureMovieEntity(MovieEntity entity)
    {
        IQueryable<MovieEntity> data = new List<MovieEntity> { entity }.AsQueryable();
        DbSet<MovieEntity> mockSet = Substitute.For<DbSet<MovieEntity>, IQueryable<MovieEntity>>();
        ((IQueryable<MovieEntity>)mockSet).Provider.Returns(data.Provider);
        ((IQueryable<MovieEntity>)mockSet).Expression.Returns(data.Expression);
        ((IQueryable<MovieEntity>)mockSet).ElementType.Returns(data.ElementType);
        ((IQueryable<MovieEntity>)mockSet).GetEnumerator().Returns(data.GetEnumerator());
        return mockSet;
    }

    private static DbSet<TEntity> ConfigureEntity<TEntity>(TEntity entity) where TEntity : class
    {
        IQueryable<TEntity> data = new List<TEntity> { entity }.AsQueryable();
        DbSet<TEntity> mockSet = Substitute.For<DbSet<TEntity>, IQueryable<TEntity>>();
        ((IQueryable<TEntity>)mockSet).Provider.Returns(data.Provider);
        ((IQueryable<TEntity>)mockSet).Expression.Returns(data.Expression);
        ((IQueryable<TEntity>)mockSet).ElementType.Returns(data.ElementType);
        ((IQueryable<TEntity>)mockSet).GetEnumerator().Returns(data.GetEnumerator());
        return mockSet;
    }
    #endregion
}
