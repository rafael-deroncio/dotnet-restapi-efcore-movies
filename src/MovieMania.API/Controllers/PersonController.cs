using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieMania.Core.Services.Interfaces;
using MovieMania.Domain.Requests;
using MovieMania.Domain.Responses;

namespace MovieMania.API.Controllers;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
[ApiController]
[Authorize]
public class PersonController(IPersonService service) : Controller
{
    private readonly IPersonService _service = service;

    [HttpGet("paged")]
    [ProducesResponseType(typeof(PaginationResponse<PersonResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status422UnprocessableEntity)]
    [AllowAnonymous]
    public async Task<IActionResult> GetPersons([FromQuery] PaginationRequest request)
        => Ok(await _service.GetPagedPersons(request));

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(PersonResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status422UnprocessableEntity)]
    [AllowAnonymous]
    public async Task<IActionResult> GetPerson([FromRoute] int id)
        => Ok(await _service.GetPersonById(id));

    [HttpPost]
    [ProducesResponseType(typeof(PersonResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status422UnprocessableEntity)]
    [AllowAnonymous]
    public async Task<IActionResult> PostPerson([FromBody] PersonRequest request)
        => Ok(await _service.CreatePerson(request));

    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(PersonResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status422UnprocessableEntity)]
    [AllowAnonymous]
    public async Task<IActionResult> PutPerson([FromRoute] int id, [FromBody] PersonRequest request)
        => Ok(await _service.UpdatePerson(id, request));

    [HttpDelete("{id:int}")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status422UnprocessableEntity)]
    [AllowAnonymous]
    public async Task<IActionResult> DeletePerson([FromRoute] int id)
        => Ok(await _service.DeletePerson(id));
}