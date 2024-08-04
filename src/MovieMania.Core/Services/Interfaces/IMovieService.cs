using MovieMania.Domain.Requests;
using MovieMania.Domain.Responses;

namespace MovieMania.Core.Services.Interfaces;

/// <summary>
/// Defines the contract for movie-related operations.
/// </summary>
public interface IMovieService
{
    /// <summary>
    /// Retrieves the information of a movie by its ID.
    /// </summary>
    /// <param name="id">The ID of the movie.</param>
    /// <param name="filter">The filter criteria to apply when retrieving the movie.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the movie response.</returns>
    Task<MovieResponse> GetMovieById(int id, MovieFilterRequest filter);

    /// <summary>
    /// Creates a new movie with the specified request data.
    /// </summary>
    /// <param name="request">The request data for creating the movie.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the movie response.</returns>
    Task<MovieResponse> CreateMovie(MovieRequest request);

    /// <summary>
    /// Updates the information of an existing movie by its ID with the specified request data.
    /// </summary>
    /// <param name="id">The ID of the movie to be updated.</param>
    /// <param name="request">The request data for updating the movie.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the movie response.</returns>
    Task<MovieResponse> UpdateMovie(int id, MovieRequest request);

    /// <summary>
    /// Deletes a movie by its ID.
    /// </summary>
    /// <param name="id">The ID of the movie to be deleted.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a boolean indicating whether the deletion was successful.</returns>
    Task<bool> DeleteMovie(int id);

    /// <summary>
    /// Retrieves a paginated list of movies based on the specified pagination request and filter criteria.
    /// </summary>
    /// <param name="request">The pagination request containing the paging parameters.</param>
    /// <param name="filter">The filter criteria to apply when retrieving the list of movies.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a paginated response of movie responses.</returns>
    Task<PaginationResponse<MovieResponse>> GetPagedMovies(PaginationRequest request, MovieFilterRequest filter);
}
