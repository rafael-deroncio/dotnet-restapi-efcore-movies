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
public class LanguageController(ILanguageService service) : Controller
{
    private readonly ILanguageService _service = service;

    #region Language
    [HttpGet("paged")]
    [ProducesResponseType(typeof(PaginationResponse<LanguageResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status422UnprocessableEntity)]
    [AllowAnonymous]
    public async Task<IActionResult> GetLanguages([FromQuery] PaginationRequest request)
        => Ok(await _service.GetPagedLanguages(request));

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(LanguageResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status422UnprocessableEntity)]
    [AllowAnonymous]
    public async Task<IActionResult> GetLanguage([FromRoute] int id)
        => Ok(await _service.GetLanguageById(id));

    [HttpPost]
    [ProducesResponseType(typeof(LanguageResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status422UnprocessableEntity)]
    [AllowAnonymous]
    public async Task<IActionResult> PostLanguage([FromBody] LanguageRequest request)
        => Ok(await _service.CreateLanguage(request));

    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(LanguageResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status422UnprocessableEntity)]
    [AllowAnonymous]
    public async Task<IActionResult> PutLanguage([FromRoute] int id, [FromBody] LanguageRequest request)
        => Ok(await _service.UpdateLanguage(id, request));

    [HttpDelete("{id:int}")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status422UnprocessableEntity)]
    [AllowAnonymous]
    public async Task<IActionResult> DeleteLanguage([FromRoute] int id)
        => Ok(await _service.DeleteLanguage(id));
    #endregion

    #region Language Role
    [HttpGet("role/paged")]
    [ProducesResponseType(typeof(PaginationResponse<LanguageRoleResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status422UnprocessableEntity)]
    [AllowAnonymous]
    public async Task<IActionResult> GetLanguageRoles([FromQuery] PaginationRequest request)
        => Ok(await _service.GetPagedLanguageRoles(request));

    [HttpGet("role/{id:int}")]
    [ProducesResponseType(typeof(LanguageRoleResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status422UnprocessableEntity)]
    [AllowAnonymous]
    public async Task<IActionResult> GetLanguageRole([FromRoute] int id)
        => Ok(await _service.GetLanguageRoleById(id));

    [HttpPost("role")]
    [ProducesResponseType(typeof(LanguageRoleResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status422UnprocessableEntity)]
    [AllowAnonymous]
    public async Task<IActionResult> PostLanguageRole([FromBody] LanguageRoleRequest request)
        => Ok(await _service.CreateLanguageRole(request));

    [HttpPut("role/{id:int}")]
    [ProducesResponseType(typeof(LanguageRoleResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status422UnprocessableEntity)]
    [AllowAnonymous]
    public async Task<IActionResult> PutLanguageRole([FromRoute] int id, [FromBody] LanguageRoleRequest request)
        => Ok(await _service.UpdateLanguageRole(id, request));

    [HttpDelete("role/{id:int}")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status422UnprocessableEntity)]
    [AllowAnonymous]
    public async Task<IActionResult> DeleteLanguageRole([FromRoute] int id)
        => Ok(await _service.DeleteLanguageRole(id));
    #endregion
}
