using MovieMania.Domain.Requests;
using MovieMania.Domain.Responses;

namespace MovieMania.Core.Services.Interfaces;

public interface IDepartmentService
{
    Task<DepartmentResponse> GetDepartmentById(int id);

    Task<DepartmentResponse> CreateDepartment(DepartmentRequest request);

    Task<DepartmentResponse> UpdateDepartment(int id, DepartmentRequest request);

    Task<bool> DeleteDepartment(int id);

    Task<PaginationResponse<DepartmentResponse>> GetPagedDepartments(PaginationRequest request);
}
