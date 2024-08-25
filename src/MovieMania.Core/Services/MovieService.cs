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
            if (_context.Movies.Where(movie => movie.Title == request.Title).Any())
                throw new EntityBadRequestException("Error on create movie entity", "Movie already registered with the same title.");

            MovieEntity entity = _mapper.Map<MovieEntity>(request);
            List<string> errors = [];

            OnCreateOrUpdateMovieProccessProductionCountries(ref entity, ref errors, false);
            OnCreateOrUpdateMovieProccessLanguages(ref entity, ref errors, false);
            OnCreateOrUpdateMovieProccessGenres(ref entity, ref errors, false);
            OnCreateOrUpdateMovieProccessKeywords(ref entity, ref errors, false);
            OnCreateOrUpdateMovieProccessCompanies(ref entity, ref errors, false);
            OnCreateOrUpdateMovieProccessCasts(ref entity, ref errors, false);
            OnCreateOrUpdateMovieProccessCrews(ref entity, ref errors, false);

            // Errors
            if (errors.Any())
                throw new EntityBadRequestException(title: "Movie Entity Error", messages: [.. errors]);

            entity.CreatedAt = DateTime.UtcNow;
            entity.UpdatedAt = DateTime.UtcNow;
            entity.ReleaseDate = DateTime.SpecifyKind(entity.ReleaseDate, DateTimeKind.Utc);

            _context.Movies.Add(entity);
            _context.SaveChanges();

            return await Task.FromResult(_mapper.Map<MovieResponse>(entity));
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
            MovieEntity entity = _context.Movies.FirstOrDefault(x => x.MovieId == id)
                ?? throw new EntityNotFoundException("Movie Not Found", $"Movie with id {id} not exists");

            _context.Movies.Remove(entity);
            _context.SaveChanges();

            return await Task.FromResult(true);
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
            MovieEntity entity = _context.Movies.FirstOrDefault(m => m.MovieId == id)
                ?? throw new EntityNotFoundException("Movie Not Found", $"Movie with id {id} not exists");

            if (!filter.AddProductionCountries) entity.ProductionCountries = [];
            if (!filter.AddCompanies) entity.Companies = [];
            if (!filter.AddLanguages) entity.Languages = [];
            if (!filter.AddGenres) entity.Genres = [];
            if (!filter.AddKeywords) entity.Keywords = [];
            if (!filter.AddCasts) entity.Casts = [];
            if (!filter.AddCrews) entity.Crews = [];
            // if (!filter.AddImages) entity.Images = [];

            return await Task.FromResult(_mapper.Map<MovieResponse>(entity));
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
                Total = _context.Movies.Count()
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
            MovieEntity entity = _context.Movies.FirstOrDefault(x => x.MovieId == id)
                ?? throw new EntityNotFoundException("Movie Not Found", $"Movie with id {id} not exists");

            if (_context.Movies.Where(movie => movie.Title == request.Title).Any())
                throw new EntityBadRequestException("Error on update movie entity", "Movie already registered with the same title.");

            entity = _mapper.Map<MovieEntity>(request);
            List<string> errors = [];

            OnCreateOrUpdateMovieProccessProductionCountries(ref entity, ref errors, true);
            OnCreateOrUpdateMovieProccessLanguages(ref entity, ref errors, true);
            OnCreateOrUpdateMovieProccessGenres(ref entity, ref errors, true);
            OnCreateOrUpdateMovieProccessKeywords(ref entity, ref errors, true);
            OnCreateOrUpdateMovieProccessCompanies(ref entity, ref errors, true);
            OnCreateOrUpdateMovieProccessCasts(ref entity, ref errors, true);
            OnCreateOrUpdateMovieProccessCrews(ref entity, ref errors, true);

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

            _context.Movies.Update(entity);
            _context.SaveChanges();

            return await Task.FromResult(_mapper.Map<MovieResponse>(entity));
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

    private void OnCreateOrUpdateMovieProccessProductionCountries(ref MovieEntity entity, ref List<string> errors, bool failFast)
    {
        if (failFast && errors.Any())
            return;

        // Country
        if (entity.ProductionCountries != null && entity.ProductionCountries.Any())
        {
            try
            {
                entity.ProductionCountries = entity.ProductionCountries.Select(x =>
                {
                    _ = _countryService.GetCountryById(x.CountryId);
                    return new ProductionCountryEntity()
                    {
                        CountryId = x.CountryId,
                        Country = _context.Countries.Find(x.CountryId),
                    };
                }
                ).ToArray();
            }
            catch (Exception exception)
            {
                if (exception is BaseException)
                    errors.Add(exception.Message);
                else if (exception.InnerException is BaseException baseException)
                    errors.Add(baseException.Message);
                else throw;
            }
        }
    }

    private void OnCreateOrUpdateMovieProccessLanguages(ref MovieEntity entity, ref List<string> errors, bool failFast)
    {
        if (failFast && errors.Any())
            return;

        // Language
        List<string> languageErrors = [];

        if (entity.Languages != null && entity.Languages.Any())
        {
            // Language
            try
            {
                entity.Languages = entity.Languages.Select(x =>
                {
                    MovieLanguageEntity language = new();

                    try
                    {
                        _ = _languageService.GetLanguageById(x.LanguageId);
                        language.LanguageId = x.LanguageId;
                        language.Language = _context.Languages.Find(x.LanguageId);
                    }
                    catch (Exception exception)
                    {
                        if (exception is BaseException)
                            languageErrors.Add(exception.Message);
                        else if (exception.InnerException is BaseException baseException)
                            languageErrors.Add(baseException.Message);
                        else throw;
                    }

                    try
                    {
                        _ = _languageService.GetLanguageRoleById(x.LanguageId);
                        language.LanguageRoleId = x.LanguageRoleId;
                        language.LanguageRole = _context.LanguageRoles.Find(x.LanguageRoleId);
                    }
                    catch (Exception exception)
                    {
                        if (exception is BaseException)
                            languageErrors.Add(exception.Message);
                        else if (exception.InnerException is BaseException baseException)
                            languageErrors.Add(baseException.Message);
                        else throw;
                    }

                    return language;
                }
                ).ToArray();

                errors.AddRange(languageErrors);
            }
            catch (Exception) { throw; }
        }
    }

    private void OnCreateOrUpdateMovieProccessGenres(ref MovieEntity entity, ref List<string> errors, bool failFast)
    {
        if (failFast && errors.Any())
            return;

        // Genres
        if (entity.Genres != null && entity.Genres.Any())
        {
            try
            {
                entity.Genres = entity.Genres.Select(x =>
                {
                    _ = _genreService.GetGenreById(x.GenreId);
                    return new MovieGenreEntity()
                    {
                        GenreId = x.GenreId,
                        Genre = _context.Genres.Find(x.GenreId)
                    };
                }
                ).ToArray();
            }
            catch (Exception exception)
            {
                if (exception is BaseException)
                    errors.Add(exception.Message);
                else if (exception.InnerException is BaseException baseException)
                    errors.Add(baseException.Message);
                else throw;
            }
        }

    }

    private void OnCreateOrUpdateMovieProccessKeywords(ref MovieEntity entity, ref List<string> errors, bool failFast)
    {
        if (failFast && errors.Any())
            return;

        // Keywords
        if (entity.Keywords != null && entity.Keywords.Any())
        {
            try
            {
                entity.Keywords = entity.Keywords.Select(x =>
                {
                    _ = _keywordService.GetKeywordById(x.KeywordId);
                    return new MovieKeywordEntity()
                    {
                        KeywordId = x.KeywordId,
                        Keyword = _context.Keywords.Find(x.KeywordId)
                    };
                }
                ).ToArray();
            }
            catch (Exception exception)
            {
                if (exception is BaseException)
                    errors.Add(exception.Message);
                else if (exception.InnerException is BaseException baseException)
                    errors.Add(baseException.Message);
                else throw;
            }
        }

    }

    private void OnCreateOrUpdateMovieProccessCompanies(ref MovieEntity entity, ref List<string> errors, bool failFast)
    {
        if (failFast && errors.Any())
            return;

        // Companies
        if (entity.Companies != null && entity.Companies.Any())
        {
            try
            {
                entity.Companies = entity.Companies.Select(x =>
                {
                    _ = _productionCompanyService.GetProductionCompanyById(x.CompanyId);
                    return new MovieCompanyEntity()
                    {
                        CompanyId = x.CompanyId,
                        ProductionCompany = _context.ProductionCompanies.Find(x.CompanyId)
                    };
                }
                ).ToArray();
            }
            catch (Exception exception)
            {
                if (exception is BaseException)
                    errors.Add(exception.Message);
                else if (exception.InnerException is BaseException baseException)
                    errors.Add(baseException.Message);
                else throw;
            }
        }

    }

    private void OnCreateOrUpdateMovieProccessCasts(ref MovieEntity entity, ref List<string> errors, bool failFast)
    {
        if (failFast && errors.Any())
            return;

        // Language
        if (entity.Casts != null && entity.Casts.Any())
        {
            try
            {
                List<string> castErros = [];

                entity.Casts = entity.Casts.Select(x =>
                {

                    MovieCastEntity cast = new()
                    {
                        CharacterName = x.CharacterName,
                        CastOrder = x.CastOrder
                    };

                    try
                    {
                        _ = _genderService.GetGenderById(x.GenderId);
                        cast.GenderId = x.GenderId;
                        cast.Gender = _context.Genders.Find(x.GenderId);
                    }
                    catch (Exception exception)
                    {
                        if (exception is BaseException)
                            castErros.Add(exception.Message);
                        else if (exception.InnerException is BaseException baseException)
                            castErros.Add(baseException.Message);
                        else throw;
                    }

                    try
                    {
                        _ = _personService.GetPersonById(x.PersonId);
                        cast.PersonId = x.PersonId;
                        cast.Person = _context.Persons.Find(x.PersonId);
                    }
                    catch (Exception exception)
                    {
                        if (exception is BaseException)
                            castErros.Add(exception.Message);
                        else if (exception.InnerException is BaseException baseException)
                            castErros.Add(baseException.Message);
                        else throw;
                    }

                    return cast;
                }
                ).ToArray();

                errors.AddRange(castErros);
            }
            catch (Exception) { throw; }
        }

    }

    private void OnCreateOrUpdateMovieProccessCrews(ref MovieEntity entity, ref List<string> errors, bool failFast)
    {
        if (failFast && errors.Any())
            return;

        // Person
        if (entity.Crews != null && entity.Crews.Any())
        {
            try
            {
                List<string> crewErrors = [];

                entity.Crews = entity.Crews.Select(x =>
                {
                    MovieCrewEntity crew = new()
                    {
                        Job = x.Job
                    };

                    try
                    {
                        _ = _departmentService.GetDepartmentById(x.DepartmentId);
                        crew.DepartmentId = x.DepartmentId;
                        crew.Department = _context.Departments.Find(x.DepartmentId);
                    }
                    catch (Exception exception)
                    {
                        if (exception is BaseException)
                            crewErrors.Add(exception.Message);
                        else if (exception.InnerException is BaseException baseException)
                            crewErrors.Add(baseException.Message);
                        else throw;
                    }

                    try
                    {
                        _ = _personService.GetPersonById(x.PersonId);
                        crew.PersonId = x.PersonId;
                        crew.Person = _context.Persons.Find(x.PersonId);
                    }
                    catch (Exception exception)
                    {
                        if (exception is BaseException)
                            crewErrors.Add(exception.Message);
                        else if (exception.InnerException is BaseException baseException)
                            crewErrors.Add(baseException.Message);
                        else throw;
                    }

                    return crew;
                }
                ).ToArray();

                errors.AddRange(crewErrors);
            }
            catch (Exception) { throw; }
        }
    }
}