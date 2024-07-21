using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CountryMania.API.Controllers;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
[ApiController]
[Authorize]
public class CountryController : Controller
{
    [HttpGet("paged")]
    [AllowAnonymous]
    public async Task<IActionResult> GetCountries([FromQuery] int page, [FromQuery] int size)
        => Ok(await Task.FromResult(new { page, size }));

    [HttpGet("id:int")]
    [AllowAnonymous]
    public async Task<IActionResult> GetCountry([FromRoute] int id)
        => Ok(await Task.FromResult(new { id }));

    [HttpPost()]
    [AllowAnonymous]
    public async Task<IActionResult> PostCountry([FromBody] object request)
        => Ok(await Task.FromResult(request));

    [HttpPut("id:int")]
    [AllowAnonymous]
    public async Task<IActionResult> PutCountry([FromRoute] int id, [FromBody] object request)
        => Ok(await Task.FromResult(new { id, request }));

    [HttpDelete("id:int")]
    [AllowAnonymous]
    public async Task<IActionResult> DeleteCountry([FromRoute] int id)
        => Ok(await Task.FromResult(new { id }));
}
