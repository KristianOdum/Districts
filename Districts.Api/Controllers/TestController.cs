using Districts.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Districts.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TestController(ITestRepository repository) : ControllerBase
{
    [HttpGet("database-connection")]
    public async Task<IActionResult> TestDatabase()
    {
        await repository.TestConnectionAsync();

        return Ok("Database connection successful.");
    }
}