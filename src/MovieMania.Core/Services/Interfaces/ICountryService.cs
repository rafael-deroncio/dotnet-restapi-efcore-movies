using MovieMania.Domain.Requests;
using MovieMania.Domain.Responses;

namespace MovieMania.Core.Services.Interfaces;

/// <summary>
/// Interface for country management services.
/// </summary>
public interface ICountryService
{
    /// <summary>
    /// Retrieves the information of a country by its ID.
    /// </summary>
    /// <param name="id">The ID of the country.</param>
    /// <returns>A task representing the asynchronous operation. The task result contains the country response.</returns>
    Task<CountryResponse> GetCountryById(int id);

    /// <summary>
    /// Creates a new country with the specified request data.
    /// </summary>
    /// <param name="request">The request data for creating the country.</param>
    /// <returns>A task representing the asynchronous operation. The task result contains the country response.</returns>
    Task<CountryResponse> CreateCountry(CountryRequest request);

    /// <summary>
    /// Updates the information of an existing country by its ID with the specified request data.
    /// </summary>
    /// <param name="id">The ID of the country to be updated.</param>
    /// <param name="request">The request data for updating the country.</param>
    /// <returns>A task representing the asynchronous operation. The task result contains the country response.</returns>
    Task<CountryResponse> UpdateCountry(int id, CountryRequest request);

    /// <summary>
    /// Deletes a country by its ID.
    /// </summary>
    /// <param name="id">The ID of the country to be deleted.</param>
    /// <returns>A task representing the asynchronous operation. The task result contains a boolean indicating whether the deletion was successful.</returns>
    Task<bool> DeleteCountry(int id);

    /// <summary>
    /// Retrieves a paginated list of countries based on the specified pagination request.
    /// </summary>
    /// <param name="request">The pagination request containing the paging parameters.</param>
    /// <returns>A task representing the asynchronous operation. The task result contains a paginated response of country responses.</returns>
    Task<PaginationResponse<CountryResponse>> GetPagedCountries(PaginationRequest request);
}
