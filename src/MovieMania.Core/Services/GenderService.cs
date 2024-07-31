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
    IDatabaseMemory memory,
    IBaseRepository<GenderEntity> repository,
    IPaginationService paginationService,
    IObjectConverter mapper
) : IGenderService
{
    private readonly ILogger<IGenderService> _logger = logger;
    private readonly IDatabaseMemory _databaseMemory = memory;
    private readonly IBaseRepository<GenderEntity> _repository = repository;
    private readonly IPaginationService _paginationService = paginationService;
    private readonly IObjectConverter _mapper = mapper;

    public async Task<GenderResponse> CreateGender(GenderRequest request)
    {
        try
        {
            if (_databaseMemory.Genders.Where(x => x.Gender == request.Gender).Any())
                throw new EntityBadRequestException("Error on create gender entity", "Gender alredy registred with name or iso code");

            GenderEntity entity = _mapper.Map<GenderEntity>(request);

            entity = await _repository.Create(entity);
            if (entity is not null) await _databaseMemory.UpdateGenders();

            return _mapper.Map<GenderResponse>(entity);
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
            GenderEntity entity = _databaseMemory.Genders.FirstOrDefault(x => x.GenderId == id);
            entity ??= await _repository.Get(new() { GenderId = id }) ??
                    throw new EntityNotFoundException("Gender Not Found", $"Gender with id {id} not exists.");

            bool result = await _repository.Delete(entity);
            if (result) await _databaseMemory.UpdateGenders();

            return result;
        }
        catch (BaseException) { throw; }
        catch (Exception exception)
        {
            _logger.LogError("Error on delete gender with id {Idntifier}. Error: {Exception}", id, exception);
            throw new EntityUnprocessableException(
                title: "Gender Entity Error",
                message: $"Unable to delete gender with id {id} at this time. Please try again.");
        }
    }

    public async Task<GenderResponse> GetGenderById(int id)
    {
        try
        {
            GenderEntity entity = _databaseMemory.Genders.FirstOrDefault(x => x.GenderId == id);
            if (entity is not null)
                return _mapper.Map<GenderResponse>(entity);

            entity = await _repository.Get(new() { GenderId = id });
            if (entity is not null)
                return _mapper.Map<GenderResponse>(entity);

            throw new EntityNotFoundException("Gender Not Found", $"Gender with id {id} not exists.");
        }
        catch (BaseException) { throw; }
        catch (Exception exception)
        {
            _logger.LogError("Error on get Gender with id {Idntifier}. Error: {Exception}", id, exception);
            throw new EntityUnprocessableException(
                title: "Gender Entity Error",
                message: $"Unable to get gender wit id {id} at this time. Please try again.");
        }
    }

    public async Task<PaginationResponse<GenderResponse>> GetPagedGenders(PaginationRequest request)
    {
        try
        {
            IEnumerable<GenderEntity> entities = _databaseMemory.Genders
                .Skip((request.Page - 1) * request.Size)
                .Take(request.Size);

            return await _paginationService.GetPagination<GenderResponse>(new()
            {
                Content = _mapper.Map<IEnumerable<GenderResponse>>(entities),
                Page = request.Page,
                Size = request.Size,
                Total = _databaseMemory.Genders.Count()
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
            GenderEntity entity = _databaseMemory.Genders.FirstOrDefault(x => x.GenderId == id);
            entity ??= await _repository.Get(new() { GenderId = id }) ??
                    throw new EntityNotFoundException("Gender Not Found", $"Gender with id {id} not exists.");

            entity.Gender = request.Gender.Trim();

            entity = await _repository.Update(entity);
            if (entity is not null) await _databaseMemory.UpdateGenders();

            return _mapper.Map<GenderResponse>(entity);
        }
        catch (BaseException) { throw; }
        catch (Exception exception)
        {
            _logger.LogError("Error on update gender with id {Idntifier} from request {Request}. Error: {Exception}", id, request, exception);
            throw new EntityUnprocessableException(
                title: "Gender Entity Error",
                message: $"Unable to update gender with id {id} at this time. Please try again.");
        }
    }
}