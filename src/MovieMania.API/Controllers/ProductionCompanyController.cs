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
public class ProductionCompanyController(IProductionCompanyService service) : Controller
{
    private readonly IProductionCompanyService _service = service;

    [HttpGet("paged")]
    [ProducesResponseType(typeof(PaginationResponse<ProductionCompanyResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status422UnprocessableEntity)]
    [AllowAnonymous]
    public async Task<IActionResult> GetProductionCompanys([FromQuery] PaginationRequest request)
        => Ok(await _service.GetPagedProductionCompanys(request));

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(ProductionCompanyResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status422UnprocessableEntity)]
    [AllowAnonymous]
    public async Task<IActionResult> GetProductionCompany([FromRoute] int id)
        => Ok(await _service.GetProductionCompanyById(id));

    [HttpPost]
    [ProducesResponseType(typeof(ProductionCompanyResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status422UnprocessableEntity)]
    [AllowAnonymous]
    public async Task<IActionResult> PostProductionCompany([FromBody] ProductionCompanyRequest request)
        => Ok(await _service.CreateProductionCompany(request));

    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(ProductionCompanyResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status422UnprocessableEntity)]
    [AllowAnonymous]
    public async Task<IActionResult> PutProductionCompany([FromRoute] int id, [FromBody] ProductionCompanyRequest request)
        => Ok(await _service.UpdateProductionCompany(id, request));

    [HttpDelete("{id:int}")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status422UnprocessableEntity)]
    [AllowAnonymous]
    public async Task<IActionResult> DeleteProductionCompany([FromRoute] int id)
        => Ok(await _service.DeleteProductionCompany(id));
}