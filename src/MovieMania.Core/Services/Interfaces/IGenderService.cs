using MovieMania.Domain.Requests;
using MovieMania.Domain.Responses;

namespace MovieMania.Core.Services.Interfaces;

/// <summary>
/// Interface for gender management services.
/// </summary>
public interface IGenderService
{
    /// <summary>
    /// Retrieves the information of a gender by its ID.
    /// </summary>
    /// <param name="id">The ID of the gender.</param>
    /// <returns>A task representing the asynchronous operation. The task result contains the gender response.</returns>
    Task<GenderResponse> GetGenderById(int id);

    /// <summary>
    /// Creates a new gender with the specified request data.
    /// </summary>
    /// <param name="request">The request data for creating the gender.</param>
    /// <returns>A task representing the asynchronous operation. The task result contains the gender response.</returns>
    Task<GenderResponse> CreateGender(GenderRequest request);

    /// <summary>
    /// Updates the information of an existing gender by its ID with the specified request data.
    /// </summary>
    /// <param name="id">The ID of the gender to be updated.</param>
    /// <param name="request">The request data for updating the gender.</param>
    /// <returns>A task representing the asynchronous operation. The task result contains the gender response.</returns>
    Task<GenderResponse> UpdateGender(int id, GenderRequest request);

    /// <summary>
    /// Deletes a gender by its ID.
    /// </summary>
    /// <param name="id">The ID of the gender to be deleted.</param>
    /// <returns>A task representing the asynchronous operation. The task result contains a boolean indicating whether the deletion was successful.</returns>
    Task<bool> DeleteGender(int id);

    /// <summary>
    /// Retrieves a paginated list of genders based on the specified pagination request.
    /// </summary>
    /// <param name="request">The pagination request containing the paging parameters.</param>
    /// <returns>A task representing the asynchronous operation. The task result contains a paginated response of gender responses.</returns>
    Task<PaginationResponse<GenderResponse>> GetPagedGenders(PaginationRequest request);
}
