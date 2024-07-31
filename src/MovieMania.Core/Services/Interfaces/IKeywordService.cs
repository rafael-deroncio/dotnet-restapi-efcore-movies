using MovieMania.Domain.Requests;
using MovieMania.Domain.Responses;

namespace MovieMania.Core.Services.Interfaces;

/// <summary>
/// Interface for keyword management services.
/// </summary>
public interface IKeywordService
{
    /// <summary>
    /// Retrieves the information of a keyword by its ID.
    /// </summary>
    /// <param name="id">The ID of the keyword.</param>
    /// <returns>A task representing the asynchronous operation. The task result contains the keyword response.</returns>
    Task<KeywordResponse> GetKeywordById(int id);

    /// <summary>
    /// Creates a new keyword with the specified request data.
    /// </summary>
    /// <param name="request">The request data for creating the keyword.</param>
    /// <returns>A task representing the asynchronous operation. The task result contains the keyword response.</returns>
    Task<KeywordResponse> CreateKeyword(KeywordRequest request);

    /// <summary>
    /// Updates the information of an existing keyword by its ID with the specified request data.
    /// </summary>
    /// <param name="id">The ID of the keyword to be updated.</param>
    /// <param name="request">The request data for updating the keyword.</param>
    /// <returns>A task representing the asynchronous operation. The task result contains the keyword response.</returns>
    Task<KeywordResponse> UpdateKeyword(int id, KeywordRequest request);

    /// <summary>
    /// Deletes a keyword by its ID.
    /// </summary>
    /// <param name="id">The ID of the keyword to be deleted.</param>
    /// <returns>A task representing the asynchronous operation. The task result contains a boolean indicating whether the deletion was successful.</returns>
    Task<bool> DeleteKeyword(int id);

    /// <summary>
    /// Retrieves a paginated list of keywords based on the specified pagination request.
    /// </summary>
    /// <param name="request">The pagination request containing the paging parameters.</param>
    /// <returns>A task representing the asynchronous operation. The task result contains a paginated response of keyword responses.</returns>
    Task<PaginationResponse<KeywordResponse>> GetPagedKeywords(PaginationRequest request);
}
