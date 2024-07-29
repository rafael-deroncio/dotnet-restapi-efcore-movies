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
public class GenreController(IGenreService service) : Controller
{
    private readonly IGenreService _service = service;

    [HttpGet("paged")]
    [ProducesResponseType(typeof(PaginationResponse<GenreResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status422UnprocessableEntity)]
    [AllowAnonymous]
    public async Task<IActionResult> GetGenres([FromQuery] PaginationRequest request)
        => Ok(await _service.GetPagedGenres(request));

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(GenreResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status422UnprocessableEntity)]
    [AllowAnonymous]
    public async Task<IActionResult> GetGenre([FromRoute] int id)
        => Ok(await _service.GetGenreById(id));

    [HttpPost]
    [ProducesResponseType(typeof(GenreResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status422UnprocessableEntity)]
    [AllowAnonymous]
    public async Task<IActionResult> PostGenre([FromBody] GenreRequest request)
        => Ok(await _service.CreateGenre(request));

    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(GenreResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status422UnprocessableEntity)]
    [AllowAnonymous]
    public async Task<IActionResult> PutGenre([FromRoute] int id, [FromBody] GenreRequest request)
        => Ok(await _service.UpdateGenre(id, request));

    [HttpDelete("{id:int}")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status422UnprocessableEntity)]
    [AllowAnonymous]
    public async Task<IActionResult> DeleteGenre([FromRoute] int id)
        => Ok(await _service.DeleteGenre(id));
}
