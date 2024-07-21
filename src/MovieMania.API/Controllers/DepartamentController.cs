using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DepartamentMania.API.Controllers;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
[ApiController]
[Authorize]
public class DepartamentController : Controller
{
    [HttpGet("paged")]
    [AllowAnonymous]
    public async Task<IActionResult> GetDepartaments([FromQuery] int page, [FromQuery] int size)
        => Ok(await Task.FromResult(new { page, size }));

    [HttpGet("{id:int}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetDepartament([FromRoute] int id)
        => Ok(await Task.FromResult(new { id }));

    [HttpPost()]
    [AllowAnonymous]
    public async Task<IActionResult> PostDepartament([FromBody] object request)
        => Ok(await Task.FromResult(request));

    [HttpPut("{id:int}")]
    [AllowAnonymous]
    public async Task<IActionResult> PutDepartament([FromRoute] int id, [FromBody] object request)
        => Ok(await Task.FromResult(new { id, request }));

    [HttpDelete("{id:int}")]
    [AllowAnonymous]
    public async Task<IActionResult> DeleteDepartament([FromRoute] int id)
        => Ok(await Task.FromResult(new { id }));
}
