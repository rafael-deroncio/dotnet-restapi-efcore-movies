using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GenreMania.API.Controllers;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
[ApiController]
[Authorize]
public class GenreController : Controller
{
    [HttpGet("paged")]
    [AllowAnonymous]
    public async Task<IActionResult> GetGenres([FromQuery] int page, [FromQuery] int size)
        => Ok(await Task.FromResult(new { page, size }));

    [HttpGet("id:int")]
    [AllowAnonymous]
    public async Task<IActionResult> GetGenre([FromRoute] int id)
        => Ok(await Task.FromResult(new { id }));

    [HttpPost()]
    [AllowAnonymous]
    public async Task<IActionResult> PostGenre([FromBody] object request)
        => Ok(await Task.FromResult(request));

    [HttpPut("id:int")]
    [AllowAnonymous]
    public async Task<IActionResult> PutGenre([FromRoute] int id, [FromBody] object request)
        => Ok(await Task.FromResult(new { id, request }));

    [HttpDelete("id:int")]
    [AllowAnonymous]
    public async Task<IActionResult> DeleteGenre([FromRoute] int id)
        => Ok(await Task.FromResult(new { id }));
}
