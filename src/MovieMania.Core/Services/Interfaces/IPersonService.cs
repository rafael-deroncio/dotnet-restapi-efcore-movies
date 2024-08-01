using MovieMania.Domain.Requests;
using MovieMania.Domain.Responses;

namespace MovieMania.Core.Services.Interfaces;

/// <summary>
/// Interface for person management services.
/// </summary>
public interface IPersonService
{
    /// <summary>
    /// Retrieves the information of a person by its ID.
    /// </summary>
    /// <param name="id">The ID of the person.</param>
    /// <returns>A task representing the asynchronous operation. The task result contains the person response.</returns>
    Task<PersonResponse> GetPersonById(int id);

    /// <summary>
    /// Creates a new person with the specified request data.
    /// </summary>
    /// <param name="request">The request data for creating the person.</param>
    /// <returns>A task representing the asynchronous operation. The task result contains the person response.</returns>
    Task<PersonResponse> CreatePerson(PersonRequest request);

    /// <summary>
    /// Updates the information of an existing person by its ID with the specified request data.
    /// </summary>
    /// <param name="id">The ID of the person to be updated.</param>
    /// <param name="request">The request data for updating the person.</param>
    /// <returns>A task representing the asynchronous operation. The task result contains the person response.</returns>
    Task<PersonResponse> UpdatePerson(int id, PersonRequest request);

    /// <summary>
    /// Deletes a person by its ID.
    /// </summary>
    /// <param name="id">The ID of the person to be deleted.</param>
    /// <returns>A task representing the asynchronous operation. The task result contains a boolean indicating whether the deletion was successful.</returns>
    Task<bool> DeletePerson(int id);

    /// <summary>
    /// Retrieves a paginated list of persons based on the specified pagination request.
    /// </summary>
    /// <param name="request">The pagination request containing the paging parameters.</param>
    /// <returns>A task representing the asynchronous operation. The task result contains a paginated response of person responses.</returns>
    Task<PaginationResponse<PersonResponse>> GetPagedPersons(PaginationRequest request);
}
