using MovieMania.Core.Services.Interfaces;
using MovieMania.Core.Requests;
using MovieMania.Domain.Responses;
using Microsoft.AspNetCore.WebUtilities;

namespace MovieMania.Core.Services;

public class PaginationService(IUriService uriService) : IPaginationService
{
    private readonly IUriService _uriService = uriService;

    public async Task<PaginationResponse<T>> GetPagination<T>(PaginationRequest<T> request)
    {
        Uri endpoint = _uriService.GetEndpoint();
        PaginationResponse<T> response = new()
        {
            Data = request.Content,

            PageNumber = request.Page,
            PageSize = request.Size,

            TotalRecords = request.Total,
            TotalPages = GetTotalPages(request.Total, request.Size),

            FirstPage = GetFirstPage(endpoint, request.Size)
        };

        response.LastPage = GetLastPage(endpoint, request.Size, response.TotalPages);

        response.NextPage = GetNextPage(endpoint, response.PageNumber, request.Size, response.TotalRecords);
        response.PreviousPage = GetPreviousPage(endpoint, response.PageNumber, request.Size, response.TotalRecords);

        return await Task.FromResult(response);
    }

    private static int GetTotalPages(int count, int size) => Convert.ToInt32((double)count / (double)size);

    private static Uri GetUriAddedQuery(string endpoint, string name, string value) => new(QueryHelpers.AddQueryString(endpoint, name, value));

    private static Uri GetFirstPage(Uri endpoint, int size, int page = 1)
    {
        Uri uriFistPage = GetUriAddedQuery(endpoint.ToString(), nameof(page), page.ToString());
        return GetUriAddedQuery(uriFistPage.ToString(), nameof(size), size.ToString());
    }

    private static Uri GetLastPage(Uri endpoint, int size, int page = 1)
    {
        Uri uriFistPage = GetUriAddedQuery(endpoint.ToString(), nameof(page), page.ToString());
        return GetUriAddedQuery(uriFistPage.ToString(), nameof(size), size.ToString());
    }

    private static Uri GetNextPage(Uri endpoint, int page, int size, int totalRecords)
    {
        if (page - 1 >= 0 && page <= totalRecords)
        {
            Uri uriNextPage = GetUriAddedQuery(endpoint.ToString(), nameof(page), (page + 1).ToString());
            return GetUriAddedQuery(uriNextPage.ToString(), nameof(size), size.ToString());
        }

        return null;

    }

    private static Uri GetPreviousPage(Uri endpoint, int page, int size, int totalRecords)
    {
        if (page >= 1 && page < totalRecords)
        {
            Uri uriNextPage = GetUriAddedQuery(endpoint.ToString(), nameof(page), (page - 1).ToString());
            return GetUriAddedQuery(uriNextPage.ToString(), nameof(size), size.ToString());
        }

        return null;

    }
}
