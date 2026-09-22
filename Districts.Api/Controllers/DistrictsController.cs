using System.ComponentModel.DataAnnotations;
using Districts.Api.Dtos;
using Districts.Application.Commands;
using Districts.Application.Queries;
using Districts.Application.Interfaces;
using Districts.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Districts.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DistrictsController(
    IDistrictRepository repository,
    GetDistrictDetailsQueryHandler getDistrictDetailsQueryHandler,
    AddSalespersonToDistrictCommandHandler addSalespersonToDistrictCommandHandler,
    RemoveSalespersonFromDistrictCommandHandler removeSalespersonFromDistrictCommandHandler,
    ILogger<DistrictsController> logger
) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<DistrictDto>>> GetDistricts()
    {
        var districts = await repository.GetDistrictsAsync();

        logger.LogInformation("Found {Count} total districts", districts.Count);

        return Ok(districts.Select(DistrictDto.FromDomain).ToList());
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<DistrictDetailsDto>> GetDistrictDetails(int id)
    {
        var query = new GetDistrictDetailsQuery(id);

        var result = await getDistrictDetailsQueryHandler.HandleAsync(query);

        if (result is null)
        {
            logger.LogWarning("District {Id} was not found", id);
            return NotFound();
        }

        return Ok(DistrictDetailsDto.FromApplication(result));
    }

    [HttpPost("{districtId:int}/salespersons")]
    public async Task<IActionResult> AddSalesperson(
        int districtId,
        AddSalespersonToDistrictDto request)
    {
        var command = new AddSalespersonToDistrict(
            districtId,
            request.SalespersonId,
            request.Role);

        logger.LogInformation(
            "Adding salesperson {SalespersonId} to district {DistrictId} as {Role}",
            request.SalespersonId,
            districtId,
            request.Role.ToString());

        await addSalespersonToDistrictCommandHandler.HandleAsync(command);

        logger.LogInformation(
            "Added salesperson {SalespersonId} to district {DistrictId} as {Role}",
            request.SalespersonId,
            districtId,
            request.Role.ToString());

        var district = await getDistrictDetailsQueryHandler.HandleAsync(
            new GetDistrictDetailsQuery(districtId));

        if (district is null) return NotFound();

        return Ok(DistrictDetailsDto.FromApplication(district));
    }

    [HttpDelete("{districtId:int}/salespersons/{salespersonId:int}")]
    public async Task<IActionResult> RemoveSalesperson(
        int districtId,
        int salespersonId,
        [FromQuery] SalespersonRole? role)
    {
        if (role is null) return BadRequest("The role query parameter is required.");

        var command = new RemoveSalespersonFromDistrict(
            districtId,
            salespersonId,
            role.Value);

        logger.LogInformation(
            "Removing salesperson {SalespersonId} from district {DistrictId} with {Role}",
            salespersonId,
            districtId,
            role.ToString());

        await removeSalespersonFromDistrictCommandHandler.HandleAsync(command);

        logger.LogInformation(
            "Removed salesperson {SalespersonId} from district {DistrictId} with {Role}",
            salespersonId,
            districtId,
            role.ToString());

        var district = await getDistrictDetailsQueryHandler.HandleAsync(
            new GetDistrictDetailsQuery(districtId));

        if (district is null) return NotFound();

        return Ok(DistrictDetailsDto.FromApplication(district));
    }
}