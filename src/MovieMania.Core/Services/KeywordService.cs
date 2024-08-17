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
    IBaseRepository<KeywordEntity> repository,
    IPaginationService paginationService,
    IObjectConverter mapper
) : IKeywordService
{
    private readonly ILogger<IKeywordService> _logger = logger;
    private readonly IBaseRepository<KeywordEntity> _repository = repository;
    private readonly IPaginationService _paginationService = paginationService;
    private readonly IObjectConverter _mapper = mapper;

    public async Task<KeywordResponse> CreateKeyword(KeywordRequest request)
    {
        try
        {
            if ((await _repository.Get()).Where(x => x.Keyword == request.Keyword).Any())
                throw new EntityBadRequestException("Error on create keyword entity", "Keyword alredy registred");

            KeywordEntity entity = _mapper.Map<KeywordEntity>(request);

            return _mapper.Map<KeywordResponse>(await _repository.Create(entity));
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
            KeywordEntity entity = await _repository.Get(new() { KeywordId = id })
                ?? throw new EntityNotFoundException("Keyword Not Found", $"Keyword with id {id} not exists.");

            return await _repository.Delete(entity);
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
            KeywordEntity entity = await _repository.Get(new() { KeywordId = id })
                ?? throw new EntityNotFoundException("Keyword Not Found", $"Keyword with id {id} not exists.");
            
            return _mapper.Map<KeywordResponse>(entity);        }
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
            IEnumerable<KeywordEntity> entities = (await _repository.Get())
                .Skip((request.Page - 1) * request.Size)
                .Take(request.Size);

            return await _paginationService.GetPagination<KeywordResponse>(new()
            {
                Content = _mapper.Map<IEnumerable<KeywordResponse>>(entities),
                Page = request.Page,
                Size = request.Size,
                Total = (await _repository.Get()).Count()
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
            KeywordEntity entity = await _repository.Get(new() { KeywordId = id })
                ?? throw new EntityNotFoundException("Keyword Not Found", $"Keyword with id {id} not exists.");

            if ((await _repository.Get()).Where(x => x.Keyword == request.Keyword).Any())
                throw new EntityBadRequestException("Error on update keyword entity", "Keyword alredy registred");

            entity.Keyword = request.Keyword.Trim();

            return _mapper.Map<KeywordResponse>(await _repository.Update(entity));
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