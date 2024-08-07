using MovieMania.Domain.Requests;

namespace MovieMania.Domain.DTOs;

public record MovieDepartment : DepartmentRequest
{
    public int Id { get; set; }
}