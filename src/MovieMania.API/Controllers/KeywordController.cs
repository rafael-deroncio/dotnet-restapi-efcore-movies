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
public class KeywordController(IKeywordService service) : Controller
{
    private readonly IKeywordService _service = service;

    [HttpGet("paged")]
    [ProducesResponseType(typeof(PaginationResponse<KeywordResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status422UnprocessableEntity)]
    [AllowAnonymous]
    public async Task<IActionResult> GetKeywords([FromQuery] PaginationRequest request)
        => Ok(await _service.GetPagedKeywords(request));

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(KeywordResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status422UnprocessableEntity)]
    [AllowAnonymous]
    public async Task<IActionResult> GetKeyword([FromRoute] int id)
        => Ok(await _service.GetKeywordById(id));

    [HttpPost]
    [ProducesResponseType(typeof(KeywordResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status422UnprocessableEntity)]
    [AllowAnonymous]
    public async Task<IActionResult> PostKeyword([FromBody] KeywordRequest request)
        => Ok(await _service.CreateKeyword(request));

    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(KeywordResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status422UnprocessableEntity)]
    [AllowAnonymous]
    public async Task<IActionResult> PutKeyword([FromRoute] int id, [FromBody] KeywordRequest request)
        => Ok(await _service.UpdateKeyword(id, request));

    [HttpDelete("{id:int}")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status422UnprocessableEntity)]
    [AllowAnonymous]
    public async Task<IActionResult> DeleteKeyword([FromRoute] int id)
        => Ok(await _service.DeleteKeyword(id));
}

