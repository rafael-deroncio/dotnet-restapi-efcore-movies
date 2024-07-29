using MovieMania.Core.Configurations.Mapper.Interfaces;
using MovieMania.Core.Contexts.Entities;
using MovieMania.Core.Exceptions;
using MovieMania.Core.Repositories.Interfaces;
using MovieMania.Core.Services.Interfaces;
using MovieMania.Domain.Requests;
using MovieMania.Domain.Responses;

namespace MovieMania.Core.Services;

public class DepartmentService(
    ILogger<IDepartmentService> logger,
    IDatabaseMemory memory,
    IBaseRepository<DepartmentEntity> repository,
    IPaginationService paginationService,
    IObjectConverter mapper
) : IDepartmentService
{
    private readonly ILogger<IDepartmentService> _logger = logger;
    private readonly IDatabaseMemory _databaseMemory = memory;
    private readonly IBaseRepository<DepartmentEntity> _repository = repository;
    private readonly IPaginationService _paginationService = paginationService;
    private readonly IObjectConverter _mapper = mapper;

    public async Task<DepartmentResponse> CreateDepartment(DepartmentRequest request)
    {
        try
        {
            if (_databaseMemory.Departments.Where(x => x.Name == request.Name).Any())
                throw new EntityBadRequestException("Error on create department entity", "Department alredy registred with name or iso code");

            DepartmentEntity entity = _mapper.Map<DepartmentEntity>(request);

            entity = await _repository.Create(entity);
            if (entity is not null) await _databaseMemory.UpdateDepartments();

            return _mapper.Map<DepartmentResponse>(entity);
        }
        catch (BaseException) { throw; }
        catch (Exception exception)
        {
            _logger.LogError("Error on create department with requet {Request}. Error: {Exception}", request, exception);
            throw new EntityUnprocessableException(
                title: "Department Entity Error",
                message: $"Unable to create a new record for department at this time. Please try again.");
        }
    }

    public async Task<bool> DeleteDepartment(int id)
    {
        try
        {
            DepartmentEntity entity = _databaseMemory.Departments.FirstOrDefault(x => x.DepartmentId == id);
            entity ??= await _repository.Get(new() { DepartmentId = id }) ??
                    throw new EntityNotFoundException("Department Not Found", $"Department with id {id} not exists.");

            bool result = await _repository.Delete(entity);
            if (result) await _databaseMemory.UpdateDepartments();

            return result;
        }
        catch (BaseException) { throw; }
        catch (Exception exception)
        {
            _logger.LogError("Error on delete department with id {Idntifier}. Error: {Exception}", id, exception);
            throw new EntityUnprocessableException(
                title: "Department Entity Error",
                message: $"Unable to delete department with id {id} at this time. Please try again.");
        }
    }

    public async Task<DepartmentResponse> GetDepartmentById(int id)
    {
        try
        {
            DepartmentEntity entity = _databaseMemory.Departments.FirstOrDefault(x => x.DepartmentId == id);
            if (entity is not null)
                return _mapper.Map<DepartmentResponse>(entity);

            entity = await _repository.Get(new() { DepartmentId = id });
            if (entity is not null)
                return _mapper.Map<DepartmentResponse>(entity);

            throw new EntityNotFoundException("Department Not Found", $"Department with id {id} not exists.");
        }
        catch (BaseException) { throw; }
        catch (Exception exception)
        {
            _logger.LogError("Error on get Department with id {Idntifier}. Error: {Exception}", id, exception);
            throw new EntityUnprocessableException(
                title: "Department Entity Error",
                message: $"Unable to get department wit id {id} at this time. Please try again.");
        }
    }

    public async Task<PaginationResponse<DepartmentResponse>> GetPagedDepartments(PaginationRequest request)
    {
        try
        {
            IEnumerable<DepartmentEntity> entities = _databaseMemory.Departments
                .Skip((request.Page - 1) * request.Size)
                .Take(request.Size);

            return await _paginationService.GetPagination<DepartmentResponse>(new()
            {
                Content = _mapper.Map<IEnumerable<DepartmentResponse>>(entities),
                Page = request.Page,
                Size = request.Size,
                Total = _databaseMemory.Departments.Count()
            });
        }
        catch (BaseException) { throw; }
        catch (Exception exception)
        {
            _logger.LogError("Error on get paged departments with request {Request}. Error: {Exception}", request, exception);
            throw new EntityUnprocessableException(
                title: "Department Entity Error",
                message: "Unable get paged records for departments at this time. Please try again.");
        }
    }

    public async Task<DepartmentResponse> UpdateDepartment(int id, DepartmentRequest request)
    {
        try
        {
            DepartmentEntity entity = _databaseMemory.Departments.FirstOrDefault(x => x.DepartmentId == id);
            entity ??= await _repository.Get(new() { DepartmentId = id }) ??
                    throw new EntityNotFoundException("Department Not Found", $"Department with id {id} not exists.");

            entity.Name = request.Name.Trim();

            entity = await _repository.Update(entity);
            if (entity is not null) await _databaseMemory.UpdateDepartments();

            return _mapper.Map<DepartmentResponse>(entity);
        }
        catch (BaseException) { throw; }
        catch (Exception exception)
        {
            _logger.LogError("Error on update department with id {Idntifier} from request {Request}. Error: {Exception}", id, request, exception);
            throw new EntityUnprocessableException(
                title: "Department Entity Error",
                message: $"Unable to update department with id {id} at this time. Please try again.");
        }
    }
}