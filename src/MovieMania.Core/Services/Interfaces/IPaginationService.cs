using MovieMania.Core.Requests;
using MovieMania.Domain.Responses;

namespace MovieMania.Core.Services.Interfaces;

/// <summary>
/// Interface for pagination services.
/// </summary>
public interface IPaginationService
{
    /// <summary>
    /// Retrieves a paginated response based on the specified pagination request.
    /// </summary>
    /// <typeparam name="T">The type of the items in the pagination request and response.</typeparam>
    /// <param name="request">The pagination request containing the paging parameters and filtering criteria.</param>
    /// <returns>A task representing the asynchronous operation. The task result contains the paginated response.</returns>
    Task<PaginationResponse<T>> GetPagination<T>(PaginationRequest<T> request);
}
