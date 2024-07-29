using MovieMania.Domain.Requests;
using MovieMania.Domain.Responses;

namespace MovieMania.Core.Services.Interfaces;

/// <summary>
/// Interface for department management services.
/// </summary>
public interface IDepartmentService
{
    /// <summary>
    /// Retrieves the information of a department by its ID.
    /// </summary>
    /// <param name="id">The ID of the department.</param>
    /// <returns>A task representing the asynchronous operation. The task result contains the department response.</returns>
    Task<DepartmentResponse> GetDepartmentById(int id);

    /// <summary>
    /// Creates a new department with the specified request data.
    /// </summary>
    /// <param name="request">The request data for creating the department.</param>
    /// <returns>A task representing the asynchronous operation. The task result contains the department response.</returns>
    Task<DepartmentResponse> CreateDepartment(DepartmentRequest request);

    /// <summary>
    /// Updates the information of an existing department by its ID with the specified request data.
    /// </summary>
    /// <param name="id">The ID of the department to be updated.</param>
    /// <param name="request">The request data for updating the department.</param>
    /// <returns>A task representing the asynchronous operation. The task result contains the department response.</returns>
    Task<DepartmentResponse> UpdateDepartment(int id, DepartmentRequest request);

    /// <summary>
    /// Deletes a department by its ID.
    /// </summary>
    /// <param name="id">The ID of the department to be deleted.</param>
    /// <returns>A task representing the asynchronous operation. The task result contains a boolean indicating whether the deletion was successful.</returns>
    Task<bool> DeleteDepartment(int id);

    /// <summary>
    /// Retrieves a paginated list of departments based on the specified pagination request.
    /// </summary>
    /// <param name="request">The pagination request containing the paging parameters.</param>
    /// <returns>A task representing the asynchronous operation. The task result contains a paginated response of department responses.</returns>
    Task<PaginationResponse<DepartmentResponse>> GetPagedDepartments(PaginationRequest request);
}
