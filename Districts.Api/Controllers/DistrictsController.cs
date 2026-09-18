using Districts.Api.Dtos;
using Districts.Application.Districts.Queries;
using Districts.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Districts.Api.Dtos;

namespace Districts.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DistrictsController(IDistrictRepository repository, GetDistrictDetailsQueryHandler getDistrictDetailsQueryHandler) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<DistrictDto>>> GetDistricts()
    {
        var districts = await repository.GetDistrictsAsync();

        var districtDtos =
            districts.Select(district => new DistrictDto(district.Id, district.Name)).ToList();

        return Ok(districtDtos);
    }
    
    [HttpGet("{id:int}")]
    public async Task<ActionResult<DistrictDetails>> GetDistrictDetails(int id)
    {
        var query = new GetDistrictDetailsQuery(id);

        var result  = await getDistrictDetailsQueryHandler.HandleAsync(query);

        if (result is null)
        {
            return NotFound();
        }
        
        var dto = new DistrictDetailsDto(
            id: result.Id,
            name: result.Name,
            salespersons: [
                .. result.Salespersons
                    .Select(SalespersonDto.FromDomain)
            ],
            stores: [
                .. result.Stores
                    .Select(StoreDto.FromDomain)
            ]);

        return Ok(dto);
    }
}