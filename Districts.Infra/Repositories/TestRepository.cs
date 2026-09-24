using Districts.Application.Interfaces;
using Microsoft.Data.SqlClient;

namespace Districts.Infra.Repositories;

public class TestRepository(string connectionString) : ITestRepository
{
    public async Task TestConnectionAsync()
    {
        await using var connection = new SqlConnection(connectionString);

        await connection.OpenAsync();
    }
}