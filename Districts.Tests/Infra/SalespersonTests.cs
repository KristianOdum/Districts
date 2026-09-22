using Districts.Domain.Models;
using Districts.Infra.Repositories;

namespace Districts.Tests.Infra;

[TestFixture]
public class SalespersonRepositoryTests
{
    private SalespersonRepository _repository = null!;

    [SetUp]
    public void SetUp()
    {
        const string connectionString =
            "Server=localhost\\MSSQLSERVER01;" +
            "Database=Districts;" +
            "User Id=admin;" +
            "Password=admin;" +
            "TrustServerCertificate=True;";

        _repository = new SalespersonRepository(connectionString);
    }

    [Test]
    public async Task GetSalespersonsAsync_ReturnsSeededSalespersons()
    {
        // Act
        var salespersons = await _repository.GetSalespersonsAsync();

        // Assert
        Assert.That(salespersons, Is.Not.Empty);

        Assert.That(
            salespersons,
            Has.Some.Matches<Salesperson>(salesperson => salesperson is
                { EmployeeNumber: "EMP00120250601", Name: "Julie Jensen" }));
    }
}