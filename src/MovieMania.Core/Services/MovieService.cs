using Microsoft.EntityFrameworkCore;
using MovieMania.Core.Configurations.Mapper.Interfaces;
using MovieMania.Core.Contexts;
using MovieMania.Core.Contexts.Entities;
using MovieMania.Core.Exceptions;
using MovieMania.Core.Extensions;
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
    private readonly DbSet<MovieEntity> _entity = context.GetEntity<MovieEntity>();
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
            if (await _entity.Where(movie => movie.Title == request.Title).AnyAsync())
                throw new EntityBadRequestException("Error on create movie entity", "Movie already registered with the same title.");

            MovieEntity entity = _mapper.Map<MovieEntity>(request);

            string[] errors = [];

            // Country
            if (entity.ProductionCountries != null && entity.ProductionCountries.Any())
            {
                try
                {
                    entity.ProductionCountries = entity.ProductionCountries.Select(x =>
                    {
                        _ = _countryService.GetCountryById(x.CountryId).GetAwaiter().GetResult();
                        return new ProductionCountryEntity()
                        {
                            CountryId = x.CountryId,
                            Country = _context.Countries.FindAsync(x.CountryId).GetAwaiter().GetResult(),
                        };
                    }
                    ).ToArray();
                }
                catch (BaseException exception)
                {
                    _ = errors.Append(exception.Message);
                }
                catch (Exception) { throw; }
            }

            // Language
            if (entity.Languages != null && entity.Languages.Any())
            {
                // Language
                try
                {
                    entity.Languages = entity.Languages.Select(x =>
                    {
                        _ = _languageService.GetLanguageById(x.LanguageId).GetAwaiter().GetResult();
                        _ = _languageService.GetLanguageRoleById(x.LanguageId).GetAwaiter().GetResult();
                        return new MovieLanguageEntity()
                        {
                            LanguageId = x.LanguageId,
                            Language = _context.Languages.FindAsync(x.LanguageId).GetAwaiter().GetResult(),

                            LanguageRoleId = x.LanguageRoleId,
                            LanguageRole = _context.LanguageRoles.FindAsync(x.LanguageRoleId).GetAwaiter().GetResult(),
                        };
                    }
                    ).ToArray();
                }
                catch (BaseException exception)
                {
                    _ = errors.Append(exception.Message);
                }
                catch (Exception) { throw; }
            }

            // Genres
            if (entity.Genres != null && entity.Genres.Any())
            {
                try
                {
                    entity.Genres = entity.Genres.Select(x =>
                    {
                        _ = _genreService.GetGenreById(x.GenreId).GetAwaiter().GetResult();
                        return new MovieGenreEntity()
                        {
                            GenreId = x.GenreId,
                            Genre = _context.Genres.FindAsync(x.GenreId).GetAwaiter().GetResult()
                        };
                    }
                    ).ToArray();
                }
                catch (BaseException exception)
                {
                    _ = errors.Append(exception.Message);
                }
                catch (Exception) { throw; }
            }

            // Keywords
            if (entity.Keywords != null && entity.Keywords.Any())
            {
                try
                {
                    entity.Keywords = entity.Keywords.Select(x =>
                    {
                        _ = _keywordService.GetKeywordById(x.KeywordId).GetAwaiter().GetResult();
                        return new MovieKeywordEntity()
                        {
                            KeywordId = x.KeywordId,
                            Keyword = _context.Keywords.FindAsync(x.KeywordId).GetAwaiter().GetResult()
                        };
                    }
                    ).ToArray();
                }
                catch (BaseException exception)
                {
                    _ = errors.Append(exception.Message);
                }
                catch (Exception) { throw; }
            }

            // Companies
            if (entity.Companies != null && entity.Companies.Any())
            {
                try
                {
                    entity.Companies = entity.Companies.Select(x =>
                    {
                        _ = _productionCompanyService.GetProductionCompanyById(x.CompanyId).GetAwaiter().GetResult();
                        return new MovieCompanyEntity()
                        {
                            CompanyId = x.CompanyId,
                            ProductionCompany = _context.ProductionCompanies.FindAsync(x.CompanyId).GetAwaiter().GetResult()
                        };
                    }
                    ).ToArray();
                }
                catch (BaseException exception)
                {
                    _ = errors.Append(exception.Message);
                }
                catch (Exception) { throw; }
            }

            // Language
            if (entity.Casts != null && entity.Casts.Any())
            {
                try
                {
                    entity.Casts = entity.Casts.Select(x =>
                    {
                        _ = _genderService.GetGenderById(x.GenderId).GetAwaiter().GetResult();
                        _ = _personService.GetPersonById(x.PersonId).GetAwaiter().GetResult();
                        return new MovieCastEntity()
                        {
                            GenderId = x.GenderId,
                            Gender = _context.Genders.FindAsync(x.GenderId).GetAwaiter().GetResult(),

                            PersonId = x.PersonId,
                            Person = _context.Persons.FindAsync(x.PersonId).GetAwaiter().GetResult(),

                            CharacterName = x.CharacterName,
                            CastOrder = x.CastOrder
                        };
                    }
                    ).ToArray();
                }
                catch (BaseException exception)
                {
                    _ = errors.Append(exception.Message);
                }
                catch (Exception) { throw; }
            }

            // Person
            if (entity.Crews != null && entity.Crews.Any())
            {
                try
                {
                    entity.Crews = entity.Crews.Select(x =>
                    {
                        _ = _departmentService.GetDepartmentById(x.DepartmentId).GetAwaiter().GetResult();
                        _ = _personService.GetPersonById(x.PersonId).GetAwaiter().GetResult();
                        return new MovieCrewEntity()
                        {
                            DepartmentId = x.DepartmentId,
                            Department = _context.Departments.FindAsync(x.DepartmentId).GetAwaiter().GetResult(),

                            PersonId = x.PersonId,
                            Person = _context.Persons.FindAsync(x.PersonId).GetAwaiter().GetResult(),

                            Job = x.Job
                        };
                    }
                    ).ToArray();
                }
                catch (BaseException exception)
                {
                    _ = errors.Append(exception.Message);
                }
                catch (Exception) { throw; }
            }

            entity.CreatedAt = DateTime.UtcNow;
            entity.UpdatedAt = DateTime.UtcNow;
            entity.ReleaseDate = DateTime.SpecifyKind(entity.ReleaseDate, DateTimeKind.Utc);

            await _entity.AddAsync(entity);
            await _context.SaveChangesAsync();

            return _mapper.Map<MovieResponse>(entity);
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
            MovieEntity entity = await _entity.FindAsync(id)
                ?? throw new EntityNotFoundException("Movie Not Found", $"Movie with id {id} not exists.");

            _entity.Remove(entity);
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
            IQueryable<MovieEntity> query = _entity;

            if (filter.AddProductionCountries)
                query = query.Include(m => m.ProductionCountries);

            if (filter.AddCompanies)
                query = query.Include(m => m.Companies);

            if (filter.AddLanguages)
                query = query.Include(m => m.Languages);

            if (filter.AddGenres)
                query = query.Include(m => m.Genres);

            if (filter.AddKeywords)
                query = query.Include(m => m.Keywords);

            if (filter.AddCasts)
                query = query.Include(m => m.Casts);

            if (filter.AddCrews)
                query = query.Include(m => m.Crews);

            // if (filter.AddImages)
            //     query = query.Include(m => m.MovieImages);

            MovieEntity entity = await query.FirstOrDefaultAsync(m => m.MovieId == id)
                ?? throw new EntityNotFoundException("Movie Not Found", $"Movie with id {id} not exists.");


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
            IQueryable<MovieEntity> query = _entity;

            if (filter.AddProductionCountries)
                query = query.Include(m => m.ProductionCountries);

            if (filter.AddCompanies)
                query = query.Include(m => m.Companies);

            if (filter.AddLanguages)
                query = query.Include(m => m.Languages);

            if (filter.AddGenres)
                query = query.Include(m => m.Genres);

            if (filter.AddKeywords)
                query = query.Include(m => m.Keywords);

            if (filter.AddCasts)
                query = query.Include(m => m.Casts);

            if (filter.AddCrews)
                query = query.Include(m => m.Crews);

            // if (filter.AddImages)
            //     query = query.Include(m => m.MovieImages);

            IEnumerable<MovieEntity> entities = query
                .Skip((request.Page - 1) * request.Size)
                .Take(request.Size);

            return await _paginationService.GetPagination<MovieResponse>(new()
            {
                Content = _mapper.Map<IEnumerable<MovieResponse>>(entities),
                Page = request.Page,
                Size = request.Size,
                Total = await _entity.CountAsync()
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
            MovieEntity entity = await _entity.FindAsync(id)
                ?? throw new EntityNotFoundException("Movie Not Found", $"Movie with id {id} not exists.");

            entity.Title = request.Title.Trim();

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
}