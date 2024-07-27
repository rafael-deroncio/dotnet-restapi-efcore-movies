using Microsoft.EntityFrameworkCore;
using MovieMania.Core.Contexts;
using MovieMania.Core.Contexts.Entities;
using MovieMania.Core.Extensions;
using MovieMania.Core.Repositories.Interfaces;
using MovieMania.Core.Services.Interfaces;
using MovieMania.Domain.Requests;
using MovieMania.Domain.Responses;

namespace MovieMania.Core.Services;

public class CountryService(
    ILogger<ICountryService> logger,
    IDatabaseMemory memory,
    IBaseRepository<CountryEntity> repository,
    IPaginationService paginationService
) : ICountryService
{
    private readonly ILogger<ICountryService> _logger = logger;
    private readonly IDatabaseMemory _databaseMemory = memory;
    private readonly IBaseRepository<CountryEntity> _repository = repository;
    private readonly IPaginationService _paginationService = paginationService;

    public async Task<CountryResponse> CreateCountry(CountryRequest request)
    {
        bool success = false;

        try
        {
            if (_databaseMemory.Countries.Where(x => x.IsoCode == request.IsoCode || x.Name == request.Name).Any())
                throw new Exception("Country alredy registred with");

            CountryEntity entity = await _repository.Create(new()
            {
                IsoCode = request.IsoCode,
                Name = request.Name,
            });

            success = true;

            return new CountryResponse()
            {
                Id = entity.CountryId,
                IsoCode = entity.IsoCode,
                Name = entity.Name,
                CreatedAt = entity.CreatedAt
            };
        }
        catch (Exception)
        {
            throw;
        }
        finally
        {
            if (success) await _databaseMemory.UpdateCountries();
        }
    }

    public async Task<bool> DeleteCountry(int id)
    {
        bool success = false;
        try
        {
            if (!_databaseMemory.Countries.Any(x => x.CountryId == id) ||
                await _repository.Get(new() { CountryId = id }) == null)
                throw new Exception("Not found");

            await _repository.Delete(new() { CountryId = id });

            success = true;

            return success;
        }
        catch (Exception)
        {
            throw;
        }
        finally
        {
            if (success) await _databaseMemory.UpdateCountries();
        }
    }

    public async Task<CountryResponse> GetCountryById(int id)
    {
        try
        {
            CountryEntity memory = _databaseMemory.Countries.FirstOrDefault(x => x.CountryId == id);
            if (memory is not null)
                return new CountryResponse()
                {
                    Id = memory.CountryId,
                    IsoCode = memory.IsoCode,
                    Name = memory.Name,
                    CreatedAt = memory.CreatedAt
                };

            CountryEntity entity = await _repository.Get(new() { CountryId = id });
            if (entity is not null)
                return new CountryResponse()
                {
                    Id = entity.CountryId,
                    IsoCode = entity.IsoCode,
                    Name = entity.Name,
                    CreatedAt = entity.CreatedAt
                };

            throw new Exception("Not found");
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<PaginationResponse<CountryResponse>> GetPagedCountries(PaginationRequest request)
    {
        try
        {
            IEnumerable<CountryEntity> entities = _databaseMemory.Countries
                .Skip((request.Page - 1) * request.Size)
                .Take(request.Size);

            IEnumerable<CountryResponse> countryResponses = entities.Select(entity => new CountryResponse
            {
                Id = entity.CountryId,
                IsoCode = entity.IsoCode,
                Name = entity.Name,
                CreatedAt = entity.CreatedAt
            });

            return await _paginationService.GetPagination<CountryResponse>(new()
            {
                Content = countryResponses,
                Page = request.Page,
                Size = request.Size,
                Total = _databaseMemory.Countries.Count()
            });
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<CountryResponse> UpdateCountry(int id, CountryRequest request)
    {
        bool success = false;
        try
        {
            if (!_databaseMemory.Countries.Any(x => x.CountryId == id) ||
                await _repository.Get(new() { CountryId = id }) == null)
                throw new Exception("Not found");

            CountryEntity entity = await _repository.Update(new()
            {
                CountryId = id,
                IsoCode = request.IsoCode,
                Name = request.Name,
            });
            success = true;

            return new CountryResponse()
            {
                Id = entity.CountryId,
                IsoCode = entity.IsoCode,
                Name = entity.Name,
                CreatedAt = entity.CreatedAt
            };
        }
        catch (Exception)
        {
            throw;
        }
        finally
        {
            if (success) await _databaseMemory.UpdateCountries();
        }
    }
}