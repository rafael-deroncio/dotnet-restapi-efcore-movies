using MovieMania.Domain.Requests;
using MovieMania.Domain.Responses;

namespace MovieMania.Core.Services.Interfaces;

/// <summary>
/// Interface for language management services.
/// </summary>
public interface ILanguageService
{
    /// <summary>
    /// Retrieves the information of a language by its ID.
    /// </summary>
    /// <param name="id">The ID of the language.</param>
    /// <returns>A task representing the asynchronous operation. The task result contains the language response.</returns>
    Task<LanguageResponse> GetLanguageById(int id);

    /// <summary>
    /// Creates a new language with the specified request data.
    /// </summary>
    /// <param name="request">The request data for creating the language.</param>
    /// <returns>A task representing the asynchronous operation. The task result contains the language response.</returns>
    Task<LanguageResponse> CreateLanguage(LanguageRequest request);

    /// <summary>
    /// Updates the information of an existing language by its ID with the specified request data.
    /// </summary>
    /// <param name="id">The ID of the language to be updated.</param>
    /// <param name="request">The request data for updating the language.</param>
    /// <returns>A task representing the asynchronous operation. The task result contains the language response.</returns>
    Task<LanguageResponse> UpdateLanguage(int id, LanguageRequest request);

    /// <summary>
    /// Deletes a language by its ID.
    /// </summary>
    /// <param name="id">The ID of the language to be deleted.</param>
    /// <returns>A task representing the asynchronous operation. The task result contains a boolean indicating whether the deletion was successful.</returns>
    Task<bool> DeleteLanguage(int id);

    /// <summary>
    /// Retrieves a paginated list of languages based on the specified pagination request.
    /// </summary>
    /// <param name="request">The pagination request containing the paging parameters.</param>
    /// <returns>A task representing the asynchronous operation. The task result contains a paginated response of language responses.</returns>
    Task<PaginationResponse<LanguageResponse>> GetPagedLanguages(PaginationRequest request);


    /// <summary>
    /// Retrieves the information of a language role by its ID.
    /// </summary>
    /// <param name="id">The ID of the language role .</param>
    /// <returns>A task representing the asynchronous operation. The task result contains the language role response.</returns>
    Task<LanguageRoleResponse> GetLanguageRoleById(int id);

    /// <summary>
    /// Creates a new language role with the specified request data.
    /// </summary>
    /// <param name="request">The request data for creating the language role.</param>
    /// <returns>A task representing the asynchronous operation. The task result contains the language role response.</returns>
    Task<LanguageRoleResponse> CreateLanguageRole(LanguageRoleRequest request);

    /// <summary>
    /// Updates the information of an existing language role by its ID with the specified request data.
    /// </summary>
    /// <param name="id">The ID of the language to be updated.</param>
    /// <param name="request">The request data for updating the language role.</param>
    /// <returns>A task representing the asynchronous operation. The task result contains the language role response.</returns>
    Task<LanguageRoleResponse> UpdateLanguageRole(int id, LanguageRoleRequest request);

    /// <summary>
    /// Deletes a language role by its ID.
    /// </summary>
    /// <param name="id">The ID of the language role to be deleted.</param>
    /// <returns>A task representing the asynchronous operation. The task result contains a boolean indicating whether the deletion was successful.</returns>
    Task<bool> DeleteLanguageRole(int id);

    /// <summary>
    /// Retrieves a paginated list of language roles based on the specified pagination request.
    /// </summary>
    /// <param name="request">The pagination request containing the paging parameters.</param>
    /// <returns>A task representing the asynchronous operation. The task result contains a paginated response of language role responses.</returns>
    Task<PaginationResponse<LanguageRoleResponse>> GetPagedLanguageRoles(PaginationRequest request);
}
