using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KeywordMania.API.Controllers;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
[ApiController]
[Authorize]
public class KeywordController : Controller
{
    [HttpGet("paged")]
    [AllowAnonymous]
    public async Task<IActionResult> GetKeywords([FromQuery] int page, [FromQuery] int size)
        => Ok(await Task.FromResult(new { page, size }));

    [HttpGet("id:int")]
    [AllowAnonymous]
    public async Task<IActionResult> GetKeyword([FromRoute] int id)
        => Ok(await Task.FromResult(new { id }));

    [HttpPost()]
    [AllowAnonymous]
    public async Task<IActionResult> PostKeyword([FromBody] object request)
        => Ok(await Task.FromResult(request));

    [HttpPut("id:int")]
    [AllowAnonymous]
    public async Task<IActionResult> PutKeyword([FromRoute] int id, [FromBody] object request)
        => Ok(await Task.FromResult(new { id, request }));

    [HttpDelete("id:int")]
    [AllowAnonymous]
    public async Task<IActionResult> DeleteKeyword([FromRoute] int id)
        => Ok(await Task.FromResult(new { id }));
}
