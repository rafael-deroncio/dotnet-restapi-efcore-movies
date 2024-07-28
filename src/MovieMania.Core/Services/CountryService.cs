using MovieMania.Core.Configurations.Mapper.Interfaces;
using MovieMania.Core.Contexts.Entities;
using MovieMania.Core.Exceptions;
using MovieMania.Core.Repositories.Interfaces;
using MovieMania.Core.Services.Interfaces;
using MovieMania.Domain.Requests;
using MovieMania.Domain.Responses;

namespace MovieMania.Core.Services;

public class CountryService(
    ILogger<ICountryService> logger,
    IDatabaseMemory memory,
    IBaseRepository<CountryEntity> repository,
    IPaginationService paginationService,
    IObjectConverter mapper
) : ICountryService
{
    private readonly ILogger<ICountryService> _logger = logger;
    private readonly IDatabaseMemory _databaseMemory = memory;
    private readonly IBaseRepository<CountryEntity> _repository = repository;
    private readonly IPaginationService _paginationService = paginationService;
    private readonly IObjectConverter _mapper = mapper;

    public async Task<CountryResponse> CreateCountry(CountryRequest request)
    {
        try
        {
            if (_databaseMemory.Countries.Where(x => x.IsoCode == request.IsoCode || x.Name == request.Name).Any())
                throw new EntityBadRequestException("Error on create country entity", "Country alredy registred with name or iso code");

            CountryEntity entity = _mapper.Map<CountryEntity>(request);

            entity = await _repository.Create(entity);
            if (entity is not null) await _databaseMemory.UpdateCountries();

            return _mapper.Map<CountryResponse>(entity);
        }
        catch (BaseException) { throw; }
        catch (Exception exception)
        {
            _logger.LogError("Error on create country with requet {Request}. Error: {Exception}", request, exception);
            throw new EntityUnprocessableException(
                title: "Country Entity Error",
                message: $"Unable to create a new record for country at this time. Please try again.");
        }
    }

    public async Task<bool> DeleteCountry(int id)
    {
        try
        {
            CountryEntity entity = _databaseMemory.Countries.FirstOrDefault(x => x.CountryId == id);
            entity ??= await _repository.Get(new() { CountryId = id }) ??
                    throw new EntityNotFoundException("Country Not Found", $"Country with id {id} not exists.");

            bool result = await _repository.Delete(entity);
            if (result) await _databaseMemory.UpdateCountries();

            return result;
        }
        catch (BaseException) { throw; }
        catch (Exception exception)
        {
            _logger.LogError("Error on delete country with id {Idntifier}. Error: {Exception}", id, exception);
            throw new EntityUnprocessableException(
                title: "Country Entity Error",
                message: $"Unable to delete country with id {id} at this time. Please try again.");
        }
    }

    public async Task<CountryResponse> GetCountryById(int id)
    {
        try
        {
            CountryEntity entity = _databaseMemory.Countries.FirstOrDefault(x => x.CountryId == id);
            if (entity is not null)
                return _mapper.Map<CountryResponse>(entity);

            entity = await _repository.Get(new() { CountryId = id });
            if (entity is not null)
                return _mapper.Map<CountryResponse>(entity);

            throw new EntityNotFoundException("Country Not Found", $"Country with id {id} not exists.");
        }
        catch (BaseException) { throw; }
        catch (Exception exception)
        {
            _logger.LogError("Error on get country with id {Idntifier}. Error: {Exception}", id, exception);
            throw new EntityUnprocessableException(
                title: "Country Entity Error",
                message: $"Unable to get Country wit id {id} at this time. Please try again.");
        }
    }

    public async Task<PaginationResponse<CountryResponse>> GetPagedCountries(PaginationRequest request)
    {
        try
        {
            IEnumerable<CountryEntity> entities = _databaseMemory.Countries
                .Skip((request.Page - 1) * request.Size)
                .Take(request.Size);

            return await _paginationService.GetPagination<CountryResponse>(new()
            {
                Content = _mapper.Map<IEnumerable<CountryResponse>>(entities),
                Page = request.Page,
                Size = request.Size,
                Total = _databaseMemory.Countries.Count()
            });
        }
        catch (BaseException) { throw; }
        catch (Exception exception)
        {
            _logger.LogError("Error on get paged countries with request {Request}. Error: {Exception}", request, exception);
            throw new EntityUnprocessableException(
                title: "Country Entity Error",
                message: "Unable get paged records for countries at this time. Please try again.");
        }
    }

    public async Task<CountryResponse> UpdateCountry(int id, CountryRequest request)
    {
        try
        {
            CountryEntity entity = _databaseMemory.Countries.FirstOrDefault(x => x.CountryId == id);
            entity ??= await _repository.Get(new() { CountryId = id }) ??
                    throw new EntityNotFoundException("Country Not Found", $"Country with id {id} not exists.");

            entity.IsoCode = request.IsoCode.Trim().ToUpper();
            entity.Name = request.Name.Trim();

            entity = await _repository.Update(entity);
            if (entity is not null) await _databaseMemory.UpdateCountries();

            return _mapper.Map<CountryResponse>(entity);
        }
        catch (BaseException) { throw; }
        catch (Exception exception)
        {
            _logger.LogError("Error on update country with id {Idntifier} from request {Request}. Error: {Exception}", id, request, exception);
            throw new EntityUnprocessableException(
                title: "Country Entity Error",
                message: $"Unable to update country with id {id} at this time. Please try again.");
        }
    }
}