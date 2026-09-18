using Districts.Core.Models;
using Districts.Infra.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Districts.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DistrictsController(DistrictRepository repository) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<DistrictDto>>> GetDistricts()
    {
        var districts = await repository.GetDistrictsAsync();

        var districtDtos = 
            districts.Select(district => new DistrictDto(id: district.Id, name: district.Name)).ToList();
        
        return Ok(districtDtos);
    }
}