using Districts.Domain.Models;
using Microsoft.Data.SqlClient;
using Dapper;

namespace Districts.Infra.Repositories;

public class DistrictRepository(string connectionString)
{
    public async Task<List<District>> GetDistrictsAsync()
    {
        const string sql = """
                               SELECT Id, Name
                               FROM dbo.District
                               ORDER BY Name;
                           """;

        await using var connection = new SqlConnection(connectionString);

        var districts = await connection.QueryAsync<District>(sql);

        return [.. districts];
    }
}