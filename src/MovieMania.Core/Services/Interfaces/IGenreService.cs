using MovieMania.Domain.Requests;
using MovieMania.Domain.Responses;

namespace MovieMania.Core.Services.Interfaces;

/// <summary>
/// Interface for genre management services.
/// </summary>
public interface IGenreService
{
    /// <summary>
    /// Retrieves the information of a genre by its ID.
    /// </summary>
    /// <param name="id">The ID of the genre.</param>
    /// <returns>A task representing the asynchronous operation. The task result contains the genre response.</returns>
    Task<GenreResponse> GetGenreById(int id);

    /// <summary>
    /// Creates a new genre with the specified request data.
    /// </summary>
    /// <param name="request">The request data for creating the genre.</param>
    /// <returns>A task representing the asynchronous operation. The task result contains the genre response.</returns>
    Task<GenreResponse> CreateGenre(GenreRequest request);

    /// <summary>
    /// Updates the information of an existing genre by its ID with the specified request data.
    /// </summary>
    /// <param name="id">The ID of the genre to be updated.</param>
    /// <param name="request">The request data for updating the genre.</param>
    /// <returns>A task representing the asynchronous operation. The task result contains the genre response.</returns>
    Task<GenreResponse> UpdateGenre(int id, GenreRequest request);

    /// <summary>
    /// Deletes a genre by its ID.
    /// </summary>
    /// <param name="id">The ID of the genre to be deleted.</param>
    /// <returns>A task representing the asynchronous operation. The task result contains a boolean indicating whether the deletion was successful.</returns>
    Task<bool> DeleteGenre(int id);

    /// <summary>
    /// Retrieves a paginated list of genres based on the specified pagination request.
    /// </summary>
    /// <param name="request">The pagination request containing the paging parameters.</param>
    /// <returns>A task representing the asynchronous operation. The task result contains a paginated response of genre responses.</returns>
    Task<PaginationResponse<GenreResponse>> GetPagedGenres(PaginationRequest request);
}
