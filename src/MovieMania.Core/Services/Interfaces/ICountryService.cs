using MovieMania.Domain.Requests;
using MovieMania.Domain.Responses;

namespace MovieMania.Core.Services.Interfaces;

public interface ICountryService
{
    Task<CountryResponse> GetCountryById(int id);
    Task<CountryResponse> CreateCountry(CountryRequest request);
    Task<CountryResponse> UpdateCountry(int id, CountryRequest request);
    Task<bool> DeleteCountry(int id);
    Task<PaginationResponse<CountryResponse>> GetPagedCountries(PaginationRequest request);
}
