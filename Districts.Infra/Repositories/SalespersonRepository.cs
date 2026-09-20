using Districts.Domain.Models;
using Microsoft.Data.SqlClient;
using Dapper;
using Districts.Application.Interfaces;

namespace Districts.Infra.Repositories;

public class SalespersonRepository(string connectionString) : ISalespersonRepository
{
    public async Task<List<Salesperson>> GetSalespersonsAsync()
    {
        const string sql = """
                           SELECT
                               Id,
                               EmployeeNumber,
                               Name
                           FROM dbo.Salesperson
                           """;

        await using var connection = new SqlConnection(connectionString);

        var salespersons = await connection.QueryAsync<Salesperson>(sql);

        return [.. salespersons];
    }
}