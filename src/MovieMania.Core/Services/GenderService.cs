using MovieMania.Core.Configurations.Mapper.Interfaces;
using MovieMania.Core.Contexts.Entities;
using MovieMania.Core.Exceptions;
using MovieMania.Core.Repositories.Interfaces;
using MovieMania.Core.Services.Interfaces;
using MovieMania.Domain.Requests;
using MovieMania.Domain.Responses;

namespace MovieMania.Core.Services;

public class GenderService(
    ILogger<IGenderService> logger,
    IBaseRepository<GenderEntity> repository,
    IPaginationService paginationService,
    IObjectConverter mapper
) : IGenderService
{
    private readonly ILogger<IGenderService> _logger = logger;
    private readonly IBaseRepository<GenderEntity> _repository = repository;
    private readonly IPaginationService _paginationService = paginationService;
    private readonly IObjectConverter _mapper = mapper;

    public async Task<GenderResponse> CreateGender(GenderRequest request)
    {
        try
        {
            if ((await _repository.Get()).Where(x => x.Gender == request.Gender).Any())
                throw new EntityBadRequestException("Error on create gender entity", "Gender alredy registred with gender");

            GenderEntity entity = _mapper.Map<GenderEntity>(request);

            return _mapper.Map<GenderResponse>(await _repository.Create(entity));
        }
        catch (BaseException) { throw; }
        catch (Exception exception)
        {
            _logger.LogError("Error on create gender with requet {Request}. Error: {Exception}", request, exception);
            throw new EntityUnprocessableException(
                title: "Gender Entity Error",
                message: $"Unable to create a new record for gender at this time. Please try again.");
        }
    }

    public async Task<bool> DeleteGender(int id)
    {
        try
        {
            GenderEntity entity = await _repository.Get(new() { GenderId = id })
                ?? throw new EntityNotFoundException("Gender Not Found", $"Gender with id {id} not exists.");

            return await _repository.Delete(entity);
        }
        catch (BaseException) { throw; }
        catch (Exception exception)
        {
            _logger.LogError("Error on delete gender with id {Identifier}. Error: {Exception}", id, exception);
            throw new EntityUnprocessableException(
                title: "Gender Entity Error",
                message: $"Unable to delete gender with id {id} at this time. Please try again.");
        }
    }

    public async Task<GenderResponse> GetGenderById(int id)
    {
        try
        {
            GenderEntity entity = await _repository.Get(new() { GenderId = id })
                ?? throw new EntityNotFoundException("Gender Not Found", $"Gender with id {id} not exists.");

            return _mapper.Map<GenderResponse>(entity);
        }
        catch (BaseException) { throw; }
        catch (Exception exception)
        {
            _logger.LogError("Error on get Gender with id {Identifier}. Error: {Exception}", id, exception);
            throw new EntityUnprocessableException(
                title: "Gender Entity Error",
                message: $"Unable to get gender wit id {id} at this time. Please try again.");
        }
    }

    public async Task<PaginationResponse<GenderResponse>> GetPagedGenders(PaginationRequest request)
    {
        try
        {
            IEnumerable<GenderEntity> entities = (await _repository.Get())
                .Skip((request.Page - 1) * request.Size)
                .Take(request.Size);

            return await _paginationService.GetPagination<GenderResponse>(new()
            {
                Content = _mapper.Map<IEnumerable<GenderResponse>>(entities),
                Page = request.Page,
                Size = request.Size,
                Total = (await _repository.Get()).Count()
            });
        }
        catch (BaseException) { throw; }
        catch (Exception exception)
        {
            _logger.LogError("Error on get paged genders with request {Request}. Error: {Exception}", request, exception);
            throw new EntityUnprocessableException(
                title: "Gender Entity Error",
                message: "Unable get paged records for genders at this time. Please try again.");
        }
    }

    public async Task<GenderResponse> UpdateGender(int id, GenderRequest request)
    {
        try
        {
            GenderEntity entity = await _repository.Get(new() { GenderId = id })
                ?? throw new EntityNotFoundException("Gender Not Found", $"Gender with id {id} not exists.");

            if ((await _repository.Get()).Where(x => x.Gender == request.Gender).Any())
                throw new EntityBadRequestException("Error on update gender entity", "Gender alredy registred with gender");

            entity.Gender = request.Gender.Trim();

            return _mapper.Map<GenderResponse>(await _repository.Update(entity));
        }
        catch (BaseException) { throw; }
        catch (Exception exception)
        {
            _logger.LogError("Error on update gender with id {Identifier} from request {Request}. Error: {Exception}", id, request, exception);
            throw new EntityUnprocessableException(
                title: "Gender Entity Error",
                message: $"Unable to update gender with id {id} at this time. Please try again.");
        }
    }
}