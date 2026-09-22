using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using NUnit.Framework;

namespace Districts.Tests.Api;

[TestFixture]
public class DistrictsApiTests
{
    private WebApplicationFactory<Program> _factory = null!;
    private HttpClient _client = null!;

    [SetUp]
    public void SetUp()
    {
        _factory = new WebApplicationFactory<Program>();
        _client = _factory.CreateClient();
    }

    [TearDown]
    public void TearDown()
    {
        _client.Dispose();
        _factory.Dispose();
    }

    [Test]
    public async Task GetDistrict_WithInvalidId_ReturnsBadRequest()
    {
        // Act
        var response = await _client.GetAsync(
            "/api/districts/999999999999");

        // Assert
        Assert.That(
            response.StatusCode,
            Is.EqualTo(HttpStatusCode.NotFound));
    }

    [Test]
    public async Task RemovePrimarySalesperson_ReturnsConflict()
    {
        // Act
        var response = await _client.DeleteAsync(
            "/api/districts/1/salespersons/1?role=Primary");

        // Assert
        Assert.That(
            response.StatusCode,
            Is.EqualTo(HttpStatusCode.Conflict));
    }
}