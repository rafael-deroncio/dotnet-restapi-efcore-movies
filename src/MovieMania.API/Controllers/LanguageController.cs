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

    [HttpGet("id:int")]
    [AllowAnonymous]
    public async Task<IActionResult> GetLanguage([FromRoute] int id)
        => Ok(await Task.FromResult(new { id }));

    [HttpPost()]
    [AllowAnonymous]
    public async Task<IActionResult> PostLanguage([FromBody] object request)
        => Ok(await Task.FromResult(request));

    [HttpPut("id:int")]
    [AllowAnonymous]
    public async Task<IActionResult> PutLanguage([FromRoute] int id, [FromBody] object request)
        => Ok(await Task.FromResult(new { id, request }));

    [HttpDelete("id:int")]
    [AllowAnonymous]
    public async Task<IActionResult> DeleteLanguage([FromRoute] int id)
        => Ok(await Task.FromResult(new { id }));


    [HttpGet("language/languageId:int/role/paged")]
    [AllowAnonymous]
    public async Task<IActionResult> GetLanguageRoles([FromRoute] int languageId, [FromQuery] int page, [FromQuery] int size)
        => Ok(await Task.FromResult(new { languageId, page, size }));

    [HttpGet("language/languageId:int/role/id:int")]
    [AllowAnonymous]
    public async Task<IActionResult> GetLanguage([FromRoute] int languageId, [FromRoute] int id)
        => Ok(await Task.FromResult(new { languageId, id }));

    [HttpPost("language/languageId:int/role/")]
    [AllowAnonymous]
    public async Task<IActionResult> PostLanguage([FromRoute] int languageId, [FromBody] object request)
        => Ok(await Task.FromResult(new { languageId, request }));

    [HttpPut("language/languageId:int/role/id:int")]
    [AllowAnonymous]
    public async Task<IActionResult> PutLanguage([FromRoute] int languageId, [FromRoute] int id, [FromBody] object request)
        => Ok(await Task.FromResult(new { languageId, id, request }));

    [HttpDelete("language/languageId:int/role/id:int")]
    [AllowAnonymous]
    public async Task<IActionResult> DeleteLanguage([FromRoute] int languageId, [FromRoute] int id)
        => Ok(await Task.FromResult(new { languageId, id }));
}
