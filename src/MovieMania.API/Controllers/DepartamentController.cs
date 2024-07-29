using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieMania.Core.Services.Interfaces;
using MovieMania.Domain.Requests;
using MovieMania.Domain.Responses;

namespace DepartmentMania.API.Controllers;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
[ApiController]
[Authorize]
public class DepartmentController(IDepartmentService service) : Controller
{
    private readonly IDepartmentService _service = service;

    [HttpGet("paged")]
    [ProducesResponseType(typeof(PaginationResponse<DepartmentResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status422UnprocessableEntity)]
    [AllowAnonymous]
    public async Task<IActionResult> GetDepartments([FromQuery] PaginationRequest request)
        => Ok(await _service.GetPagedDepartments(request));

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(DepartmentResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status422UnprocessableEntity)]
    [AllowAnonymous]
    public async Task<IActionResult> GetDepartment([FromRoute] int id)
        => Ok(await _service.GetDepartmentById(id));

    [HttpPost]
    [ProducesResponseType(typeof(DepartmentResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status422UnprocessableEntity)]
    [AllowAnonymous]
    public async Task<IActionResult> PostDepartment([FromBody] DepartmentRequest request)
        => Ok(await _service.CreateDepartment(request));

    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(DepartmentResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status422UnprocessableEntity)]
    [AllowAnonymous]
    public async Task<IActionResult> PutDepartment([FromRoute] int id, [FromBody] DepartmentRequest request)
        => Ok(await _service.UpdateDepartment(id, request));

    [HttpDelete("{id:int}")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status422UnprocessableEntity)]
    [AllowAnonymous]
    public async Task<IActionResult> DeleteDepartment([FromRoute] int id)
        => Ok(await _service.DeleteDepartment(id));
}
