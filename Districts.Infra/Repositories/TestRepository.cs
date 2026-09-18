using Microsoft.Data.SqlClient;

namespace Districts.Infra.Repositories;

public class TestRepository(string connectionString)
{
    public async Task TestConnectionAsync()
    {
        await using var connection = new SqlConnection(connectionString);

        await connection.OpenAsync();
    }
}