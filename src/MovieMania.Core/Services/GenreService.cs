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
    IBaseRepository<GenreEntity> repository,
    IPaginationService paginationService,
    IObjectConverter mapper
) : IGenreService
{
    private readonly ILogger<IGenreService> _logger = logger;
    private readonly IBaseRepository<GenreEntity> _repository = repository;
    private readonly IPaginationService _paginationService = paginationService;
    private readonly IObjectConverter _mapper = mapper;

    public async Task<GenreResponse> CreateGenre(GenreRequest request)
    {
        try
        {
            if ((await _repository.Get()).Where(x => x.Name == request.Name).Any())
                throw new EntityBadRequestException("Error on create genre entity", "Genre alredy registred with name or iso code");

            GenreEntity entity = _mapper.Map<GenreEntity>(request);

            return _mapper.Map<GenreResponse>(await _repository.Create(entity));
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
            GenreEntity entity = await _repository.Get(new() { GenreId = id })
                ?? throw new EntityNotFoundException("Genre Not Found", $"Genre with id {id} not exists.");

            return await _repository.Delete(entity);
        }
        catch (BaseException) { throw; }
        catch (Exception exception)
        {
            _logger.LogError("Error on delete genre with id {Identifier}. Error: {Exception}", id, exception);
            throw new EntityUnprocessableException(
                title: "Genre Entity Error",
                message: $"Unable to delete genre with id {id} at this time. Please try again.");
        }
    }

    public async Task<GenreResponse> GetGenreById(int id)
    {
        try
        {
            GenreEntity entity = await _repository.Get(new() { GenreId = id })
                ?? throw new EntityNotFoundException("Genre Not Found", $"Genre with id {id} not exists.");

            return _mapper.Map<GenreResponse>(entity);
        }
        catch (BaseException) { throw; }
        catch (Exception exception)
        {
            _logger.LogError("Error on get Genre with id {Identifier}. Error: {Exception}", id, exception);
            throw new EntityUnprocessableException(
                title: "Genre Entity Error",
                message: $"Unable to get genre wit id {id} at this time. Please try again.");
        }
    }

    public async Task<PaginationResponse<GenreResponse>> GetPagedGenres(PaginationRequest request)
    {
        try
        {
            IEnumerable<GenreEntity> entities = (await _repository.Get())
                .Skip((request.Page - 1) * request.Size)
                .Take(request.Size);

            return await _paginationService.GetPagination<GenreResponse>(new()
            {
                Content = _mapper.Map<IEnumerable<GenreResponse>>(entities),
                Page = request.Page,
                Size = request.Size,
                Total = (await _repository.Get()).Count()
            });
        }
        catch (BaseException) { throw; }
        catch (Exception exception)
        {
            _logger.LogError("Error on get paged genres with request {Request}. Error: {Exception}", request, exception);
            throw new EntityUnprocessableException(
                title: "Genre Entity Error",
                message: "Unable get paged records for genres at this time. Please try again.");
        }
    }

    public async Task<GenreResponse> UpdateGenre(int id, GenreRequest request)
    {
        try
        {
            GenreEntity entity = await _repository.Get(new() { GenreId = id })
                ?? throw new EntityNotFoundException("Genre Not Found", $"Genre with id {id} not exists.");

            entity.Name = request.Name.Trim();

            return _mapper.Map<GenreResponse>(await _repository.Update(entity));
        }
        catch (BaseException) { throw; }
        catch (Exception exception)
        {
            _logger.LogError("Error on update genre with id {Identifier} from request {Request}. Error: {Exception}", id, request, exception);
            throw new EntityUnprocessableException(
                title: "Genre Entity Error",
                message: $"Unable to update genre with id {id} at this time. Please try again.");
        }
    }
}