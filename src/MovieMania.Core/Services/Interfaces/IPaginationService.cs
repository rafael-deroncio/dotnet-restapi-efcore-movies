using MovieMania.Core.Requests;
using MovieMania.Domain.Responses;

namespace MovieMania.Core.Services.Interfaces;

public interface IPaginationService
{
    Task<PaginationResponse<T>> GetPagination<T>(PaginationRequest<T> request);
}