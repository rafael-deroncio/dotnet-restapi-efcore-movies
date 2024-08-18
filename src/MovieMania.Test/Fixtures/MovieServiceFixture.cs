using AutoFixture;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MovieMania.Core.Configurations.Mapper;
using MovieMania.Core.Configurations.Mapper.Interfaces;
using MovieMania.Core.Contexts;
using MovieMania.Core.Contexts.Entities;
using MovieMania.Core.Repositories.Interfaces;
using MovieMania.Core.Requests;
using MovieMania.Core.Services;
using MovieMania.Core.Services.Interfaces;
using MovieMania.Domain.Requests;
using MovieMania.Domain.Responses;
using NSubstitute;
using NSubstitute.Core;

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
        _logger = Substitute.For<ILogger<IMovieService>>();
        _context = Substitute.For<MovieManiaContext>();
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

    private Fixture GetFixture()
    {
        Fixture fixture = new();
        fixture.Behaviors
            .OfType<ThrowingRecursionBehavior>()
            .ToList()
            .ForEach(b => fixture.Behaviors.Remove(b));

        fixture.Behaviors.Add(new OmitOnRecursionBehavior());

        return fixture;
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
    public IEnumerable<ICall> ProductionCompanyServiceCalls() => _personService.ReceivedCalls();
    #endregion

    #region Fixtures

    #endregion

    #region Mocks
    public MovieRequest MovieRequestMock() => _fixture.Create<MovieRequest>();
    public PaginationRequest PaginationRequestMock() => _fixture.Create<PaginationRequest>();
    public MovieFilterRequest MovieFilterRequestMock()
    {
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
}
