using MovieMania.Core.Configurations.Mapper.Interfaces;
using MovieMania.Core.Contexts.Entities;
using MovieMania.Core.Exceptions;
using MovieMania.Core.Repositories.Interfaces;
using MovieMania.Core.Services.Interfaces;
using MovieMania.Domain.Requests;
using MovieMania.Domain.Responses;

namespace MovieMania.Core.Services;

public class ProductionCompanyService(
    ILogger<IProductionCompanyService> logger,
    IDatabaseMemory memory,
    IBaseRepository<ProductionCompanyEntity> repository,
    IPaginationService paginationService,
    IObjectConverter mapper
) : IProductionCompanyService
{
    private readonly ILogger<IProductionCompanyService> _logger = logger;
    private readonly IDatabaseMemory _databaseMemory = memory;
    private readonly IBaseRepository<ProductionCompanyEntity> _repository = repository;
    private readonly IPaginationService _paginationService = paginationService;
    private readonly IObjectConverter _mapper = mapper;

    public async Task<ProductionCompanyResponse> CreateProductionCompany(ProductionCompanyRequest request)
    {
        try
        {
            if (_databaseMemory.ProductionCompanies.Where(x => x.Company == request.Company).Any())
                throw new EntityBadRequestException("Error on create production company entity", "Production Company alredy registred with name or iso code");

            ProductionCompanyEntity entity = _mapper.Map<ProductionCompanyEntity>(request);

            entity = await _repository.Create(entity);
            if (entity is not null) await _databaseMemory.UpdateProductionCompanies();

            return _mapper.Map<ProductionCompanyResponse>(entity);
        }
        catch (BaseException) { throw; }
        catch (Exception exception)
        {
            _logger.LogError("Error on create production company with requet {Request}. Error: {Exception}", request, exception);
            throw new EntityUnprocessableException(
                title: "Production Company Entity Error",
                message: $"Unable to create a new record for production company at this time. Please try again.");
        }
    }

    public async Task<bool> DeleteProductionCompany(int id)
    {
        try
        {
            ProductionCompanyEntity entity = _databaseMemory.ProductionCompanies.FirstOrDefault(x => x.CompanyId == id);
            entity ??= await _repository.Get(new() { CompanyId = id }) ??
                    throw new EntityNotFoundException("Production Company Not Found", $"Production Company with id {id} not exists.");

            bool result = await _repository.Delete(entity);
            if (result) await _databaseMemory.UpdateProductionCompanies();

            return result;
        }
        catch (BaseException) { throw; }
        catch (Exception exception)
        {
            _logger.LogError("Error on delete production company with id {Identifier}. Error: {Exception}", id, exception);
            throw new EntityUnprocessableException(
                title: "Production Company Entity Error",
                message: $"Unable to delete production company with id {id} at this time. Please try again.");
        }
    }

    public async Task<ProductionCompanyResponse> GetProductionCompanyById(int id)
    {
        try
        {
            ProductionCompanyEntity entity = _databaseMemory.ProductionCompanies.FirstOrDefault(x => x.CompanyId == id);
            if (entity is not null)
                return _mapper.Map<ProductionCompanyResponse>(entity);

            entity = await _repository.Get(new() { CompanyId = id });
            if (entity is not null)
                return _mapper.Map<ProductionCompanyResponse>(entity);

            throw new EntityNotFoundException("Production Company Not Found", $"Production Company with id {id} not exists.");
        }
        catch (BaseException) { throw; }
        catch (Exception exception)
        {
            _logger.LogError("Error on get Production Company with id {Identifier}. Error: {Exception}", id, exception);
            throw new EntityUnprocessableException(
                title: "Production Company Entity Error",
                message: $"Unable to get production company wit id {id} at this time. Please try again.");
        }
    }

    public async Task<PaginationResponse<ProductionCompanyResponse>> GetPagedProductionCompanys(PaginationRequest request)
    {
        try
        {
            IEnumerable<ProductionCompanyEntity> entities = _databaseMemory.ProductionCompanies
                .Skip((request.Page - 1) * request.Size)
                .Take(request.Size);

            return await _paginationService.GetPagination<ProductionCompanyResponse>(new()
            {
                Content = _mapper.Map<IEnumerable<ProductionCompanyResponse>>(entities),
                Page = request.Page,
                Size = request.Size,
                Total = _databaseMemory.ProductionCompanies.Count()
            });
        }
        catch (BaseException) { throw; }
        catch (Exception exception)
        {
            _logger.LogError("Error on get paged production companys with request {Request}. Error: {Exception}", request, exception);
            throw new EntityUnprocessableException(
                title: "Production Company Entity Error",
                message: "Unable get paged records for production companys at this time. Please try again.");
        }
    }

    public async Task<ProductionCompanyResponse> UpdateProductionCompany(int id, ProductionCompanyRequest request)
    {
        try
        {
            ProductionCompanyEntity entity = _databaseMemory.ProductionCompanies.FirstOrDefault(x => x.CompanyId == id);
            entity ??= await _repository.Get(new() { CompanyId = id }) ??
                    throw new EntityNotFoundException("Production Company Not Found", $"Production Company with id {id} not exists.");

            entity.Company = request.Company.Trim();

            entity = await _repository.Update(entity);
            if (entity is not null) await _databaseMemory.UpdateProductionCompanies();

            return _mapper.Map<ProductionCompanyResponse>(entity);
        }
        catch (BaseException) { throw; }
        catch (Exception exception)
        {
            _logger.LogError("Error on update production company with id {Identifier} from request {Request}. Error: {Exception}", id, request, exception);
            throw new EntityUnprocessableException(
                title: "Production Company Entity Error",
                message: $"Unable to update production company with id {id} at this time. Please try again.");
        }
    }
}