using Microsoft.EntityFrameworkCore;
using MovieMania.Core.Configurations.Mapper.Interfaces;
using MovieMania.Core.Contexts.Entities;
using MovieMania.Core.Exceptions;
using MovieMania.Core.Extensions;
using MovieMania.Core.Services.Interfaces;
using MovieMania.Domain.Requests;
using MovieMania.Domain.Responses;

namespace MovieMania.Core.Services;

public class MovieService(
    ILogger<IMovieService> logger,
    DbContext context,
    IPaginationService paginationService,
    IObjectConverter mapper
) : IMovieService
{
    private readonly ILogger<IMovieService> _logger = logger;
    private readonly DbContext _context = context;
    private readonly DbSet<MovieEntity> _entity = context.GetEntity<MovieEntity>();
    private readonly IPaginationService _paginationService = paginationService;
    private readonly IObjectConverter _mapper = mapper;

    public async Task<MovieResponse> CreateMovie(MovieRequest request)
    {
        try
        {
            if (await _entity.Where(movie => movie.Title == request.Title).AnyAsync())
                throw new EntityBadRequestException("Error on create movie entity", "Movie already registered with the same name or iso code");

            MovieEntity entity = _mapper.Map<MovieEntity>(request);

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
            MovieEntity entity = await _entity.FindAsync(id)
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
            IEnumerable<MovieEntity> entities = _entity
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