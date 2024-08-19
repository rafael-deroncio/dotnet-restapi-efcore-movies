using Microsoft.EntityFrameworkCore;
using MovieMania.Core.Configurations.Mapper.Interfaces;
using MovieMania.Core.Contexts;
using MovieMania.Core.Contexts.Entities;
using MovieMania.Core.Exceptions;
using MovieMania.Core.Services.Interfaces;
using MovieMania.Domain.Requests;
using MovieMania.Domain.Responses;

namespace MovieMania.Core.Services;

public class MovieService(
    ILogger<IMovieService> logger,
    MovieManiaContext context,
    IPaginationService paginationService,
    IObjectConverter mapper,
    ICountryService countryService,
    IDepartmentService departmentService,
    IGenderService genderService,
    IGenreService genreService,
    IKeywordService keywordService,
    ILanguageService languageService,
    IPersonService personService,
    IProductionCompanyService productionCompanyService
) : IMovieService
{
    private readonly ILogger<IMovieService> _logger = logger;
    private readonly MovieManiaContext _context = context;
    private readonly IPaginationService _paginationService = paginationService;
    private readonly IObjectConverter _mapper = mapper;
    private readonly ICountryService _countryService = countryService;
    private readonly IDepartmentService _departmentService = departmentService;
    private readonly IGenderService _genderService = genderService;
    private readonly IGenreService _genreService = genreService;
    private readonly IKeywordService _keywordService = keywordService;
    private readonly ILanguageService _languageService = languageService;
    private readonly IPersonService _personService = personService;
    private readonly IProductionCompanyService _productionCompanyService = productionCompanyService;

    public async Task<MovieResponse> CreateMovie(MovieRequest request)
    {
        try
        {
            if (await _context.Movies.Where(movie => movie.Title == request.Title).AnyAsync())
                throw new EntityBadRequestException("Error on create movie entity", "Movie already registered with the same title.");

            MovieEntity entity = _mapper.Map<MovieEntity>(request);
            List<string> errors = [];

            OnCreateOrUpdateMovieProccessProductionCountries(ref entity, ref errors);
            OnCreateOrUpdateMovieProccessLanguages(ref entity, ref errors);
            OnCreateOrUpdateMovieProccessGenres(ref entity, ref errors);
            OnCreateOrUpdateMovieProccessKeywords(ref entity, ref errors);
            OnCreateOrUpdateMovieProccessCompanies(ref entity, ref errors);
            OnCreateOrUpdateMovieProccessCasts(ref entity, ref errors);
            OnCreateOrUpdateMovieProccessCrews(ref entity, ref errors);

            // Errors
            if (errors.Any())
                throw new EntityBadRequestException(title: "Movie Entity Error", messages: [.. errors]);

            entity.CreatedAt = DateTime.UtcNow;
            entity.UpdatedAt = DateTime.UtcNow;
            entity.ReleaseDate = DateTime.SpecifyKind(entity.ReleaseDate, DateTimeKind.Utc);

            await _context.Movies.AddAsync(entity);
            await _context.SaveChangesAsync();

            MovieResponse response = _mapper.Map<MovieResponse>(entity);

            return response;
        }
        catch (BaseException)
        {
            throw;
        }
        catch (Exception exception)
        {
            _logger.LogError("Error on create movie with request {Request}. Error: {Exception}", request, exception);
            throw new EntityUnprocessableException(
                title: "Movie Entity Error",
                message: "Unable to create a new record for the movie at this time. Please try again.");
        }
    }

    public async Task<bool> DeleteMovie(int id)
    {
        try
        {
            MovieEntity entity = await _context.Movies.FirstOrDefaultAsync(x => x.MovieId == id)
                ?? throw new EntityNotFoundException("Movie Not Found", $"Movie with id {id} not exists.");

            _context.Movies.Remove(entity);
            await _context.SaveChangesAsync();

            return true;
        }
        catch (BaseException) { throw; }
        catch (Exception exception)
        {
            _logger.LogError("Error on delete movie with id {Identifier}. Error: {Exception}", id, exception);
            throw new EntityUnprocessableException(
                title: "Movie Entity Error",
                message: $"Unable to delete movie with id {id} at this time. Please try again.");
        }
    }

    public async Task<MovieResponse> GetMovieById(int id, MovieFilterRequest filter)
    {
        try
        {
            MovieEntity entity = await _context.Movies.FirstOrDefaultAsync(m => m.MovieId == id)
                ?? throw new EntityNotFoundException("Movie Not Found", $"Movie with id {id} not exists.");

            if (!filter.AddProductionCountries) entity.ProductionCountries = [];
            if (!filter.AddCompanies) entity.Companies = [];
            if (!filter.AddLanguages) entity.Languages = [];
            if (!filter.AddGenres) entity.Genres = [];
            if (!filter.AddKeywords) entity.Keywords = [];
            if (!filter.AddCasts) entity.Casts = [];
            if (!filter.AddCrews) entity.Crews = [];
            // if (!filter.AddImages) entity.Images = [];

            return _mapper.Map<MovieResponse>(entity);
        }
        catch (BaseException) { throw; }
        catch (Exception exception)
        {
            _logger.LogError("Error on get Movie with id {Identifier}. Error: {Exception}", id, exception);
            throw new EntityUnprocessableException(
                title: "Movie Entity Error",
                message: $"Unable to get movie wit id {id} at this time. Please try again.");
        }
    }

    public async Task<PaginationResponse<MovieResponse>> GetPagedMovies(PaginationRequest request, MovieFilterRequest filter)
    {
        try
        {
            IQueryable<MovieEntity> query = _context.Movies.AsTracking();

            IEnumerable<MovieEntity> entities = query
                .Skip((request.Page - 1) * request.Size)
                .Take(request.Size);

            entities = entities.Select(entity =>
            {
                if (!filter.AddProductionCountries) entity.ProductionCountries = [];
                if (!filter.AddCompanies) entity.Companies = [];
                if (!filter.AddLanguages) entity.Languages = [];
                if (!filter.AddGenres) entity.Genres = [];
                if (!filter.AddKeywords) entity.Keywords = [];
                if (!filter.AddCasts) entity.Casts = [];
                if (!filter.AddCrews) entity.Crews = [];
                // if (!filter.AddImages) entity.Images = [];
                return entity;
            });

            return await _paginationService.GetPagination<MovieResponse>(new()
            {
                Content = _mapper.Map<IEnumerable<MovieResponse>>(entities),
                Page = request.Page,
                Size = request.Size,
                Total = await _context.Movies.CountAsync()
            });
        }
        catch (BaseException) { throw; }
        catch (Exception exception)
        {
            _logger.LogError("Error on get paged movies with request {Request}. Error: {Exception}", request, exception);
            throw new EntityUnprocessableException(
                title: "Movie Entity Error",
                message: "Unable get paged records for movies at this time. Please try again.");
        }
    }

    public async Task<MovieResponse> UpdateMovie(int id, MovieRequest request)
    {
        try
        {
            MovieEntity entity = await _context.Movies.FindAsync(id)
                ?? throw new EntityNotFoundException("Movie Not Found", $"Movie with id {id} not exists.");

            entity = _mapper.Map<MovieEntity>(request);
            List<string> errors = [];

            OnCreateOrUpdateMovieProccessProductionCountries(ref entity, ref errors);
            OnCreateOrUpdateMovieProccessLanguages(ref entity, ref errors);
            OnCreateOrUpdateMovieProccessGenres(ref entity, ref errors);
            OnCreateOrUpdateMovieProccessKeywords(ref entity, ref errors);
            OnCreateOrUpdateMovieProccessCompanies(ref entity, ref errors);
            OnCreateOrUpdateMovieProccessCasts(ref entity, ref errors);
            OnCreateOrUpdateMovieProccessCrews(ref entity, ref errors);

            // Errors
            if (errors.Any())
                throw new EntityBadRequestException(title: "Movie Entity Error", messages: [.. errors]);

            entity.MovieId = id;
            entity.Title = request.Title.Trim();
            entity.Budget = request.Budget;
            entity.Homepage = request.Homepage;
            entity.Overview = request.Overview;
            entity.Popularity = request.Popularity;
            entity.ReleaseDate = DateTime.SpecifyKind(request.ReleaseDate, DateTimeKind.Utc);
            entity.Revenue = request.Revenue;
            entity.Runtime = request.Runtime;
            entity.Status = request.Status;
            entity.Tagline = request.Tagline;
            entity.VotesAvg = request.VotesAverage;
            entity.VotesCount = request.VotesCount;
            entity.UpdatedAt = DateTime.UtcNow;

            entity = _context.Movies.Update(entity).Entity;
            await _context.SaveChangesAsync();

            return _mapper.Map<MovieResponse>(entity);
        }
        catch (BaseException) { throw; }
        catch (Exception exception)
        {
            _logger.LogError("Error on update movie with id {Identifier} from request {Request}. Error: {Exception}", id, request, exception);
            throw new EntityUnprocessableException(
                title: "Movie Entity Error",
                message: $"Unable to update movie with id {id} at this time. Please try again.");
        }
    }

    private void OnCreateOrUpdateMovieProccessProductionCountries(ref MovieEntity entity, ref List<string> errors)
    {
        // Country
        if (entity.ProductionCountries != null && entity.ProductionCountries.Any())
        {
            try
            {
                entity.ProductionCountries = entity.ProductionCountries.Select(x =>
                {
                    _ = _countryService.GetCountryById(x.CountryId).Result;
                    return new ProductionCountryEntity()
                    {
                        CountryId = x.CountryId,
                        Country = _context.Countries.FindAsync(x.CountryId).Result,
                    };
                }
                ).ToArray();
            }
            catch (Exception exception)
            {
                if (exception.InnerException is BaseException baseException)
                    errors.Add(baseException.Message);
                else throw;
            }
        }
    }

    private void OnCreateOrUpdateMovieProccessLanguages(ref MovieEntity entity, ref List<string> errors)
    {
        // Language
        if (entity.Languages != null && entity.Languages.Any())
        {
            // Language
            try
            {
                entity.Languages = entity.Languages.Select(x =>
                {
                    _ = _languageService.GetLanguageById(x.LanguageId).Result;
                    _ = _languageService.GetLanguageRoleById(x.LanguageId).Result;
                    return new MovieLanguageEntity()
                    {
                        LanguageId = x.LanguageId,
                        Language = _context.Languages.FindAsync(x.LanguageId).Result,

                        LanguageRoleId = x.LanguageRoleId,
                        LanguageRole = _context.LanguageRoles.FindAsync(x.LanguageRoleId).Result,
                    };
                }
                ).ToArray();
            }
            catch (Exception exception)
            {
                if (exception.InnerException is BaseException baseException)
                    errors.Add(baseException.Message);
                else throw;
            }
        }
    }

    private void OnCreateOrUpdateMovieProccessGenres(ref MovieEntity entity, ref List<string> errors)
    {
        // Genres
        if (entity.Genres != null && entity.Genres.Any())
        {
            try
            {
                entity.Genres = entity.Genres.Select(x =>
                {
                    _ = _genreService.GetGenreById(x.GenreId).Result;
                    return new MovieGenreEntity()
                    {
                        GenreId = x.GenreId,
                        Genre = _context.Genres.FindAsync(x.GenreId).Result
                    };
                }
                ).ToArray();
            }
            catch (Exception exception)
            {
                if (exception.InnerException is BaseException baseException)
                    errors.Add(baseException.Message);
                else throw;
            }
        }

    }

    private void OnCreateOrUpdateMovieProccessKeywords(ref MovieEntity entity, ref List<string> errors)
    {
        // Keywords
        if (entity.Keywords != null && entity.Keywords.Any())
        {
            try
            {
                entity.Keywords = entity.Keywords.Select(x =>
                {
                    _ = _keywordService.GetKeywordById(x.KeywordId).Result;
                    return new MovieKeywordEntity()
                    {
                        KeywordId = x.KeywordId,
                        Keyword = _context.Keywords.FindAsync(x.KeywordId).Result
                    };
                }
                ).ToArray();
            }
            catch (Exception exception)
            {
                if (exception.InnerException is BaseException baseException)
                    errors.Add(baseException.Message);
                else throw;
            }
        }

    }

    private void OnCreateOrUpdateMovieProccessCompanies(ref MovieEntity entity, ref List<string> errors)
    {
        // Companies
        if (entity.Companies != null && entity.Companies.Any())
        {
            try
            {
                entity.Companies = entity.Companies.Select(x =>
                {
                    _ = _productionCompanyService.GetProductionCompanyById(x.CompanyId).Result;
                    return new MovieCompanyEntity()
                    {
                        CompanyId = x.CompanyId,
                        ProductionCompany = _context.ProductionCompanies.FindAsync(x.CompanyId).Result
                    };
                }
                ).ToArray();
            }
            catch (Exception exception)
            {
                if (exception.InnerException is BaseException baseException)
                    errors.Add(baseException.Message);
                else throw;
            }
        }

    }

    private void OnCreateOrUpdateMovieProccessCasts(ref MovieEntity entity, ref List<string> errors)
    {
        // Language
        if (entity.Casts != null && entity.Casts.Any())
        {
            try
            {
                entity.Casts = entity.Casts.Select(x =>
                {
                    _ = _genderService.GetGenderById(x.GenderId).Result;
                    _ = _personService.GetPersonById(x.PersonId).Result;
                    return new MovieCastEntity()
                    {
                        GenderId = x.GenderId,
                        Gender = _context.Genders.FindAsync(x.GenderId).Result,

                        PersonId = x.PersonId,
                        Person = _context.Persons.FindAsync(x.PersonId).Result,

                        CharacterName = x.CharacterName,
                        CastOrder = x.CastOrder
                    };
                }
                ).ToArray();
            }
            catch (Exception exception)
            {
                if (exception.InnerException is BaseException baseException)
                    errors.Add(baseException.Message);
                else throw;
            }
        }

    }

    private void OnCreateOrUpdateMovieProccessCrews(ref MovieEntity entity, ref List<string> errors)
    {
        // Person
        if (entity.Crews != null && entity.Crews.Any())
        {
            try
            {
                entity.Crews = entity.Crews.Select(x =>
                {
                    _ = _departmentService.GetDepartmentById(x.DepartmentId).Result;
                    _ = _personService.GetPersonById(x.PersonId).Result;
                    return new MovieCrewEntity()
                    {
                        DepartmentId = x.DepartmentId,
                        Department = _context.Departments.FindAsync(x.DepartmentId).Result,

                        PersonId = x.PersonId,
                        Person = _context.Persons.FindAsync(x.PersonId).Result,

                        Job = x.Job
                    };
                }
                ).ToArray();
            }
            catch (Exception exception)
            {
                if (exception.InnerException is BaseException baseException)
                    errors.Add(baseException.Message);
                else throw;
            }
        }

    }
}