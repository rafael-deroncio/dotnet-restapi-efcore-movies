using MovieMania.Core.Configurations.Mapper.Interfaces;
using MovieMania.Core.Contexts.Entities;
using MovieMania.Core.Exceptions;
using MovieMania.Core.Repositories.Interfaces;
using MovieMania.Core.Services.Interfaces;
using MovieMania.Domain.Requests;
using MovieMania.Domain.Responses;

namespace MovieMania.Core.Services;

public class GenreService(
    ILogger<IGenreService> logger,
    IDatabaseMemory memory,
    IBaseRepository<GenreEntity> repository,
    IPaginationService paginationService,
    IObjectConverter mapper
) : IGenreService
{
    private readonly ILogger<IGenreService> _logger = logger;
    private readonly IDatabaseMemory _databaseMemory = memory;
    private readonly IBaseRepository<GenreEntity> _repository = repository;
    private readonly IPaginationService _paginationService = paginationService;
    private readonly IObjectConverter _mapper = mapper;

    public async Task<GenreResponse> CreateGenre(GenreRequest request)
    {
        try
        {
            if (_databaseMemory.Genres.Where(x => x.Name == request.Name).Any())
                throw new EntityBadRequestException("Error on create genre entity", "Genre alredy registred with name or iso code");

            GenreEntity entity = _mapper.Map<GenreEntity>(request);

            entity = await _repository.Create(entity);
            if (entity is not null) await _databaseMemory.UpdateGenres();

            return _mapper.Map<GenreResponse>(entity);
        }
        catch (BaseException) { throw; }
        catch (Exception exception)
        {
            _logger.LogError("Error on create genre with requet {Request}. Error: {Exception}", request, exception);
            throw new EntityUnprocessableException(
                title: "Genre Entity Error",
                message: $"Unable to create a new record for genre at this time. Please try again.");
        }
    }

    public async Task<bool> DeleteGenre(int id)
    {
        try
        {
            GenreEntity entity = _databaseMemory.Genres.FirstOrDefault(x => x.GenreId == id);
            entity ??= await _repository.Get(new() { GenreId = id }) ??
                    throw new EntityNotFoundException("Genre Not Found", $"Genre with id {id} not exists.");

            bool result = await _repository.Delete(entity);
            if (result) await _databaseMemory.UpdateGenres();

            return result;
        }
        catch (BaseException) { throw; }
        catch (Exception exception)
        {
            _logger.LogError("Error on delete genre with id {Idntifier}. Error: {Exception}", id, exception);
            throw new EntityUnprocessableException(
                title: "Genre Entity Error",
                message: $"Unable to delete genre with id {id} at this time. Please try again.");
        }
    }

    public async Task<GenreResponse> GetGenreById(int id)
    {
        try
        {
            GenreEntity entity = _databaseMemory.Genres.FirstOrDefault(x => x.GenreId == id);
            if (entity is not null)
                return _mapper.Map<GenreResponse>(entity);

            entity = await _repository.Get(new() { GenreId = id });
            if (entity is not null)
                return _mapper.Map<GenreResponse>(entity);

            throw new EntityNotFoundException("Genre Not Found", $"Genre with id {id} not exists.");
        }
        catch (BaseException) { throw; }
        catch (Exception exception)
        {
            _logger.LogError("Error on get Genre with id {Idntifier}. Error: {Exception}", id, exception);
            throw new EntityUnprocessableException(
                title: "Genre Entity Error",
                message: $"Unable to get genre wit id {id} at this time. Please try again.");
        }
    }

    public async Task<PaginationResponse<GenreResponse>> GetPagedGenres(PaginationRequest request)
    {
        try
        {
            IEnumerable<GenreEntity> entities = _databaseMemory.Genres
                .Skip((request.Page - 1) * request.Size)
                .Take(request.Size);

            return await _paginationService.GetPagination<GenreResponse>(new()
            {
                Content = _mapper.Map<IEnumerable<GenreResponse>>(entities),
                Page = request.Page,
                Size = request.Size,
                Total = _databaseMemory.Genres.Count()
            });
        }
        catch (BaseException) { throw; }
        catch (Exception exception)
        {
            _logger.LogError("Error on get paged departments with request {Request}. Error: {Exception}", request, exception);
            throw new EntityUnprocessableException(
                title: "Genre Entity Error",
                message: "Unable get paged records for departments at this time. Please try again.");
        }
    }

    public async Task<GenreResponse> UpdateGenre(int id, GenreRequest request)
    {
        try
        {
            GenreEntity entity = _databaseMemory.Genres.FirstOrDefault(x => x.GenreId == id);
            entity ??= await _repository.Get(new() { GenreId = id }) ??
                    throw new EntityNotFoundException("Genre Not Found", $"Genre with id {id} not exists.");

            entity.Name = request.Name.Trim();

            entity = await _repository.Update(entity);
            if (entity is not null) await _databaseMemory.UpdateGenres();

            return _mapper.Map<GenreResponse>(entity);
        }
        catch (BaseException) { throw; }
        catch (Exception exception)
        {
            _logger.LogError("Error on update genre with id {Idntifier} from request {Request}. Error: {Exception}", id, request, exception);
            throw new EntityUnprocessableException(
                title: "Genre Entity Error",
                message: $"Unable to update genre with id {id} at this time. Please try again.");
        }
    }
}