using Districts.Infra.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Districts.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TestController(TestRepository repository) : ControllerBase
{
    [HttpGet("database-connection")]
    public async Task<IActionResult> TestDatabase()
    {
        await repository.TestConnectionAsync();

        return Ok("Database connection successful.");
    }
}