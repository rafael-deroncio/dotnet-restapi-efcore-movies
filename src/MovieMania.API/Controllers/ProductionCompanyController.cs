using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ProductionCompanyMania.API.Controllers;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
[ApiController]
[Authorize]
public class ProductionCompanyController : Controller
{
    [HttpGet("paged")]
    [AllowAnonymous]
    public async Task<IActionResult> GetProductionCompanies([FromQuery] int page, [FromQuery] int size)
        => Ok(await Task.FromResult(new { page, size }));

    [HttpGet("id:int")]
    [AllowAnonymous]
    public async Task<IActionResult> GetProductionCompany([FromRoute] int id)
        => Ok(await Task.FromResult(new { id }));

    [HttpPost()]
    [AllowAnonymous]
    public async Task<IActionResult> PostProductionCompany([FromBody] object request)
        => Ok(await Task.FromResult(request));

    [HttpPut("id:int")]
    [AllowAnonymous]
    public async Task<IActionResult> PutProductionCompany([FromRoute] int id, [FromBody] object request)
        => Ok(await Task.FromResult(new { id, request }));

    [HttpDelete("id:int")]
    [AllowAnonymous]
    public async Task<IActionResult> DeleteProductionCompany([FromRoute] int id)
        => Ok(await Task.FromResult(new { id }));
}
