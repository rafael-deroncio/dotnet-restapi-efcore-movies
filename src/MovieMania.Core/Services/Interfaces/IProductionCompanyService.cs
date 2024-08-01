using MovieMania.Domain.Requests;
using MovieMania.Domain.Responses;

namespace MovieMania.Core.Services.Interfaces;

/// <summary>
/// Interface for production company management services.
/// </summary>
public interface IProductionCompanyService
{
    /// <summary>
    /// Retrieves the information of a production company by its ID.
    /// </summary>
    /// <param name="id">The ID of the production company.</param>
    /// <returns>A task representing the asynchronous operation. The task result contains the production company response.</returns>
    Task<ProductionCompanyResponse> GetProductionCompanyById(int id);

    /// <summary>
    /// Creates a new production company with the specified request data.
    /// </summary>
    /// <param name="request">The request data for creating the production company.</param>
    /// <returns>A task representing the asynchronous operation. The task result contains the production company response.</returns>
    Task<ProductionCompanyResponse> CreateProductionCompany(ProductionCompanyRequest request);

    /// <summary>
    /// Updates the information of an existing production company by its ID with the specified request data.
    /// </summary>
    /// <param name="id">The ID of the production company to be updated.</param>
    /// <param name="request">The request data for updating the production company.</param>
    /// <returns>A task representing the asynchronous operation. The task result contains the production company response.</returns>
    Task<ProductionCompanyResponse> UpdateProductionCompany(int id, ProductionCompanyRequest request);

    /// <summary>
    /// Deletes a production company by its ID.
    /// </summary>
    /// <param name="id">The ID of the production company to be deleted.</param>
    /// <returns>A task representing the asynchronous operation. The task result contains a boolean indicating whether the deletion was successful.</returns>
    Task<bool> DeleteProductionCompany(int id);

    /// <summary>
    /// Retrieves a paginated list of production companys based on the specified pagination request.
    /// </summary>
    /// <param name="request">The pagination request containing the paging parameters.</param>
    /// <returns>A task representing the asynchronous operation. The task result contains a paginated response of production company responses.</returns>
    Task<PaginationResponse<ProductionCompanyResponse>> GetPagedProductionCompanys(PaginationRequest request);
}
