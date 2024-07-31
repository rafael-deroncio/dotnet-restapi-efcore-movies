using MovieMania.Core.Configurations.Mapper.Interfaces;
using MovieMania.Core.Contexts.Entities;
using MovieMania.Core.Exceptions;
using MovieMania.Core.Repositories.Interfaces;
using MovieMania.Core.Services.Interfaces;
using MovieMania.Domain.Requests;
using MovieMania.Domain.Responses;

namespace MovieMania.Core.Services;

public class KeywordService(
    ILogger<IKeywordService> logger,
    IDatabaseMemory memory,
    IBaseRepository<KeywordEntity> repository,
    IPaginationService paginationService,
    IObjectConverter mapper
) : IKeywordService
{
    private readonly ILogger<IKeywordService> _logger = logger;
    private readonly IDatabaseMemory _databaseMemory = memory;
    private readonly IBaseRepository<KeywordEntity> _repository = repository;
    private readonly IPaginationService _paginationService = paginationService;
    private readonly IObjectConverter _mapper = mapper;

    public async Task<KeywordResponse> CreateKeyword(KeywordRequest request)
    {
        try
        {
            if (_databaseMemory.Keywords.Where(x => x.Keyword == request.Keyword).Any())
                throw new EntityBadRequestException("Error on create keyword entity", "Keyword alredy registred with name or iso code");

            KeywordEntity entity = _mapper.Map<KeywordEntity>(request);

            entity = await _repository.Create(entity);
            if (entity is not null) await _databaseMemory.UpdateKeywords();

            return _mapper.Map<KeywordResponse>(entity);
        }
        catch (BaseException) { throw; }
        catch (Exception exception)
        {
            _logger.LogError("Error on create keyword with requet {Request}. Error: {Exception}", request, exception);
            throw new EntityUnprocessableException(
                title: "Keyword Entity Error",
                message: $"Unable to create a new record for keyword at this time. Please try again.");
        }
    }

    public async Task<bool> DeleteKeyword(int id)
    {
        try
        {
            KeywordEntity entity = _databaseMemory.Keywords.FirstOrDefault(x => x.KeywordId == id);
            entity ??= await _repository.Get(new() { KeywordId = id }) ??
                    throw new EntityNotFoundException("Keyword Not Found", $"Keyword with id {id} not exists.");

            bool result = await _repository.Delete(entity);
            if (result) await _databaseMemory.UpdateKeywords();

            return result;
        }
        catch (BaseException) { throw; }
        catch (Exception exception)
        {
            _logger.LogError("Error on delete keyword with id {Identifier}. Error: {Exception}", id, exception);
            throw new EntityUnprocessableException(
                title: "Keyword Entity Error",
                message: $"Unable to delete keyword with id {id} at this time. Please try again.");
        }
    }

    public async Task<KeywordResponse> GetKeywordById(int id)
    {
        try
        {
            KeywordEntity entity = _databaseMemory.Keywords.FirstOrDefault(x => x.KeywordId == id);
            if (entity is not null)
                return _mapper.Map<KeywordResponse>(entity);

            entity = await _repository.Get(new() { KeywordId = id });
            if (entity is not null)
                return _mapper.Map<KeywordResponse>(entity);

            throw new EntityNotFoundException("Keyword Not Found", $"Keyword with id {id} not exists.");
        }
        catch (BaseException) { throw; }
        catch (Exception exception)
        {
            _logger.LogError("Error on get Keyword with id {Identifier}. Error: {Exception}", id, exception);
            throw new EntityUnprocessableException(
                title: "Keyword Entity Error",
                message: $"Unable to get keyword wit id {id} at this time. Please try again.");
        }
    }

    public async Task<PaginationResponse<KeywordResponse>> GetPagedKeywords(PaginationRequest request)
    {
        try
        {
            IEnumerable<KeywordEntity> entities = _databaseMemory.Keywords
                .Skip((request.Page - 1) * request.Size)
                .Take(request.Size);

            return await _paginationService.GetPagination<KeywordResponse>(new()
            {
                Content = _mapper.Map<IEnumerable<KeywordResponse>>(entities),
                Page = request.Page,
                Size = request.Size,
                Total = _databaseMemory.Keywords.Count()
            });
        }
        catch (BaseException) { throw; }
        catch (Exception exception)
        {
            _logger.LogError("Error on get paged keywords with request {Request}. Error: {Exception}", request, exception);
            throw new EntityUnprocessableException(
                title: "Keyword Entity Error",
                message: "Unable get paged records for keywords at this time. Please try again.");
        }
    }

    public async Task<KeywordResponse> UpdateKeyword(int id, KeywordRequest request)
    {
        try
        {
            KeywordEntity entity = _databaseMemory.Keywords.FirstOrDefault(x => x.KeywordId == id);
            entity ??= await _repository.Get(new() { KeywordId = id }) ??
                    throw new EntityNotFoundException("Keyword Not Found", $"Keyword with id {id} not exists.");

            entity.Keyword = request.Keyword.Trim();

            entity = await _repository.Update(entity);
            if (entity is not null) await _databaseMemory.UpdateKeywords();

            return _mapper.Map<KeywordResponse>(entity);
        }
        catch (BaseException) { throw; }
        catch (Exception exception)
        {
            _logger.LogError("Error on update keyword with id {Identifier} from request {Request}. Error: {Exception}", id, request, exception);
            throw new EntityUnprocessableException(
                title: "Keyword Entity Error",
                message: $"Unable to update keyword with id {id} at this time. Please try again.");
        }
    }
}