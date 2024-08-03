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
    IBaseRepository<ProductionCompanyEntity> repository,
    IPaginationService paginationService,
    IObjectConverter mapper
) : IProductionCompanyService
{
    private readonly ILogger<IProductionCompanyService> _logger = logger;
    private readonly IBaseRepository<ProductionCompanyEntity> _repository = repository;
    private readonly IPaginationService _paginationService = paginationService;
    private readonly IObjectConverter _mapper = mapper;

    public async Task<ProductionCompanyResponse> CreateProductionCompany(ProductionCompanyRequest request)
    {
        try
        {
            if ((await _repository.Get()).Where(x => x.Company == request.Company).Any())
                throw new EntityBadRequestException("Error on create production company entity", "ProductionCompany alredy registred with name or iso code");

            ProductionCompanyEntity entity = _mapper.Map<ProductionCompanyEntity>(request);

            return _mapper.Map<ProductionCompanyResponse>(await _repository.Create(entity));
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
            ProductionCompanyEntity entity = await _repository.Get(new() { CompanyId = id })
                ?? throw new EntityNotFoundException("Production Company Not Found", $"Production Company with id {id} not exists.");

            return await _repository.Delete(entity);
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
            ProductionCompanyEntity entity = await _repository.Get(new() { CompanyId = id })
                ?? throw new EntityNotFoundException("Production Company Not Found", $"Production Company with id {id} not exists.");

            return _mapper.Map<ProductionCompanyResponse>(entity);
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
            IEnumerable<ProductionCompanyEntity> entities = (await _repository.Get())
                .Skip((request.Page - 1) * request.Size)
                .Take(request.Size);

            return await _paginationService.GetPagination<ProductionCompanyResponse>(new()
            {
                Content = _mapper.Map<IEnumerable<ProductionCompanyResponse>>(entities),
                Page = request.Page,
                Size = request.Size,
                Total = (await _repository.Get()).Count()
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
            ProductionCompanyEntity entity = await _repository.Get(new() { CompanyId = id })
                ?? throw new EntityNotFoundException("Production Company Not Found", $"Production Company with id {id} not exists.");

            entity.Company = request.Company.Trim();

            return _mapper.Map<ProductionCompanyResponse>(await _repository.Update(entity));
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