using Districts.Api.Dtos;
using Districts.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Districts.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SalespersonsController(
    ISalespersonRepository repository,
    ILogger<SalespersonsController> logger
) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<SalespersonDto>>> GetSalespersons()
    {
        var salespersons = await repository.GetSalespersonsAsync();

        logger.LogInformation("Found {Count} total salespersons", salespersons.Count);

        return Ok(salespersons.Select(SalespersonDto.FromDomain).ToList());
    }
}