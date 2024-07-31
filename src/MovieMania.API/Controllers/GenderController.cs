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
public class GenderController(IGenderService service) : Controller
{
    private readonly IGenderService _service = service;

    [HttpGet("paged")]
    [ProducesResponseType(typeof(PaginationResponse<GenderResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status422UnprocessableEntity)]
    [AllowAnonymous]
    public async Task<IActionResult> GetGenders([FromQuery] PaginationRequest request)
        => Ok(await _service.GetPagedGenders(request));

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(GenderResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status422UnprocessableEntity)]
    [AllowAnonymous]
    public async Task<IActionResult> GetGender([FromRoute] int id)
        => Ok(await _service.GetGenderById(id));

    [HttpPost]
    [ProducesResponseType(typeof(GenderResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status422UnprocessableEntity)]
    [AllowAnonymous]
    public async Task<IActionResult> PostGender([FromBody] GenderRequest request)
        => Ok(await _service.CreateGender(request));

    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(GenderResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status422UnprocessableEntity)]
    [AllowAnonymous]
    public async Task<IActionResult> PutGender([FromRoute] int id, [FromBody] GenderRequest request)
        => Ok(await _service.UpdateGender(id, request));

    [HttpDelete("{id:int}")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status422UnprocessableEntity)]
    [AllowAnonymous]
    public async Task<IActionResult> DeleteGender([FromRoute] int id)
        => Ok(await _service.DeleteGender(id));
}
