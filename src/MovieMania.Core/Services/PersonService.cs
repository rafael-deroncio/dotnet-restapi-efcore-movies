using MovieMania.Core.Configurations.Mapper.Interfaces;
using MovieMania.Core.Contexts.Entities;
using MovieMania.Core.Exceptions;
using MovieMania.Core.Repositories.Interfaces;
using MovieMania.Core.Services.Interfaces;
using MovieMania.Domain.Requests;
using MovieMania.Domain.Responses;

namespace MovieMania.Core.Services;

public class PersonService(
    ILogger<IPersonService> logger,
    IBaseRepository<PersonEntity> repository,
    IPaginationService paginationService,
    IObjectConverter mapper
) : IPersonService
{
    private readonly ILogger<IPersonService> _logger = logger;
    private readonly IBaseRepository<PersonEntity> _repository = repository;
    private readonly IPaginationService _paginationService = paginationService;
    private readonly IObjectConverter _mapper = mapper;

    public async Task<PersonResponse> CreatePerson(PersonRequest request)
    {
        try
        {
            if ((await _repository.Get()).Where(x => x.Name == request.Name).Any())
                throw new EntityBadRequestException("Error on create person entity", "Person alredy registred");

            PersonEntity entity = _mapper.Map<PersonEntity>(request);

            return _mapper.Map<PersonResponse>(await _repository.Create(entity));
        }
        catch (BaseException) { throw; }
        catch (Exception exception)
        {
            _logger.LogError("Error on create person with requet {Request}. Error: {Exception}", request, exception);
            throw new EntityUnprocessableException(
                title: "Person Entity Error",
                message: $"Unable to create a new record for person at this time. Please try again.");
        }
    }

    public async Task<bool> DeletePerson(int id)
    {
        try
        {
            PersonEntity entity = await _repository.Get(new() { PersonId = id })
                ?? throw new EntityNotFoundException("Person Not Found", $"Person with id {id} not exists.");

            return await _repository.Delete(entity);
        }
        catch (BaseException) { throw; }
        catch (Exception exception)
        {
            _logger.LogError("Error on delete person with id {Identifier}. Error: {Exception}", id, exception);
            throw new EntityUnprocessableException(
                title: "Person Entity Error",
                message: $"Unable to delete person with id {id} at this time. Please try again.");
        }
    }

    public async Task<PersonResponse> GetPersonById(int id)
    {
        try
        {
            PersonEntity entity = await _repository.Get(new() { PersonId = id })
                ?? throw new EntityNotFoundException("Person Not Found", $"Person with id {id} not exists.");

            return _mapper.Map<PersonResponse>(entity);
        }
        catch (BaseException) { throw; }
        catch (Exception exception)
        {
            _logger.LogError("Error on get Person with id {Identifier}. Error: {Exception}", id, exception);
            throw new EntityUnprocessableException(
                title: "Person Entity Error",
                message: $"Unable to get person wit id {id} at this time. Please try again.");
        }
    }

    public async Task<PaginationResponse<PersonResponse>> GetPagedPersons(PaginationRequest request)
    {
        try
        {
            IEnumerable<PersonEntity> entities = (await _repository.Get())
                .Skip((request.Page - 1) * request.Size)
                .Take(request.Size);

            return await _paginationService.GetPagination<PersonResponse>(new()
            {
                Content = _mapper.Map<IEnumerable<PersonResponse>>(entities),
                Page = request.Page,
                Size = request.Size,
                Total = (await _repository.Get()).Count()
            });
        }
        catch (BaseException) { throw; }
        catch (Exception exception)
        {
            _logger.LogError("Error on get paged persons with request {Request}. Error: {Exception}", request, exception);
            throw new EntityUnprocessableException(
                title: "Person Entity Error",
                message: "Unable get paged records for persons at this time. Please try again.");
        }
    }

    public async Task<PersonResponse> UpdatePerson(int id, PersonRequest request)
    {
        try
        {
            PersonEntity entity = await _repository.Get(new() { PersonId = id })
                ?? throw new EntityNotFoundException("Person Not Found", $"Person with id {id} not exists.");
            
            if ((await _repository.Get()).Where(x => x.Name == request.Name).Any())
                throw new EntityBadRequestException("Error on update person entity", "Person alredy registred");
            
            entity.Name = request.Name.Trim();

            return _mapper.Map<PersonResponse>(await _repository.Update(entity));
        }
        catch (BaseException) { throw; }
        catch (Exception exception)
        {
            _logger.LogError("Error on update person with id {Identifier} from request {Request}. Error: {Exception}", id, request, exception);
            throw new EntityUnprocessableException(
                title: "Person Entity Error",
                message: $"Unable to update person with id {id} at this time. Please try again.");
        }
    }
}