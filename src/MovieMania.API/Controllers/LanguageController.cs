using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LanguageMania.API.Controllers;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
[ApiController]
[Authorize]
public class LanguageController : Controller
{
    [HttpGet("paged")]
    [AllowAnonymous]
    public async Task<IActionResult> GetLanguages([FromQuery] int page, [FromQuery] int size)
        => Ok(await Task.FromResult(new { page, size }));

    [HttpGet("{id:int}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetLanguage(int id)
        => Ok(await Task.FromResult(new { id }));

    [HttpPost()]
    [AllowAnonymous]
    public async Task<IActionResult> PostLanguage([FromBody] object request)
        => Ok(await Task.FromResult(request));

    [HttpPut("{id:int}")]
    [AllowAnonymous]
    public async Task<IActionResult> PutLanguage([FromRoute] int id, [FromBody] object request)
        => Ok(await Task.FromResult(new { id, request }));

    [HttpDelete("{id:int}")]
    [AllowAnonymous]
    public async Task<IActionResult> DeleteLanguage([FromRoute] int id)
        => Ok(await Task.FromResult(new { id }));


    [HttpGet("{languageId:int}/role/paged")]
    [AllowAnonymous]
    public async Task<IActionResult> GetLanguageRoles([FromRoute] int languageId, [FromQuery] int page, [FromQuery] int size)
        => Ok(await Task.FromResult(new { languageId, page, size }));

    [HttpGet("{languageId:int}/role/{roleId:int}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetLanguageRole([FromRoute] int languageId, [FromRoute] int roleId)
        => Ok(await Task.FromResult(new { languageId, roleId }));

    [HttpPost("{languageId:int}/role")]
    [AllowAnonymous]
    public async Task<IActionResult> PostLanguageRole([FromRoute] int languageId, [FromBody] object request)
        => Ok(await Task.FromResult(new { languageId, request }));

    [HttpPut("{languageId:int}/role/{roleId:int}")]
    [AllowAnonymous]
    public async Task<IActionResult> PutLanguageRole([FromRoute] int languageId, [FromRoute] int roleId, [FromBody] object request)
        => Ok(await Task.FromResult(new { languageId, roleId, request }));

    [HttpDelete("{languageId:int}/role/{roleId:int}")]
    [AllowAnonymous]
    public async Task<IActionResult> DeleteLanguageRole([FromRoute] int languageId, [FromRoute] int roleId)
        => Ok(await Task.FromResult(new { languageId, roleId }));
}
