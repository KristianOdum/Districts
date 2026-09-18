using Districts.Api.Dtos;
using Districts.Application.Districts.Queries;
using Districts.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Districts.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DistrictsController(
    IDistrictRepository repository, 
    GetDistrictDetailsQueryHandler getDistrictDetailsQueryHandler,
    ILogger<DistrictsController> logger
    ) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<DistrictDto>>> GetDistricts()
    {
        var districts = await repository.GetDistrictsAsync();
        
        logger.LogInformation("Found {Count} total districts", districts.Count);

        return Ok(districts.Select(DistrictDto.FromDomain));
    }
    
    [HttpGet("{id:int}")]
    public async Task<ActionResult<DistrictDetailsDto>> GetDistrictDetails(int id)
    {
        var query = new GetDistrictDetailsQuery(id);

        var result  = await getDistrictDetailsQueryHandler.HandleAsync(query);

        if (result is null)
        {
            logger.LogWarning("District {Id} was not found", id);
            return NotFound();
        }

        return Ok(DistrictDetailsDto.FromApplication(result));
    }
}