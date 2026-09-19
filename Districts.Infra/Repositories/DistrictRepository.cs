using Districts.Domain.Models;
using Microsoft.Data.SqlClient;
using Dapper;
using Districts.Application.Interfaces;
using Districts.Domain;

namespace Districts.Infra.Repositories;

public class DistrictRepository(string connectionString) : IDistrictRepository
{
    // TODO: Implement "Each district ALWAYS has a SINGLE primary salesperson." with test
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

    public async Task<District?> GetDistrictAsync(int districtId)
    {
        const string sql = """
                           SELECT Id, Name
                           FROM dbo.District
                           WHERE Id = @DistrictId
                           """;
        
        await using var connection = new SqlConnection(connectionString);
        
        return await connection.QuerySingleOrDefaultAsync<District>(
            sql,
            new { DistrictId = districtId });
    }
    
    public async Task<List<Salesperson>> GetSalespersonsAsync(int districtId)
    {
        const string sql = """
                           SELECT
                               s.Id,
                               s.Name,
                               ds.Role
                           FROM dbo.DistrictSalesperson ds
                           INNER JOIN dbo.Salesperson s
                               ON s.Id = ds.SalespersonId
                           WHERE ds.DistrictId = @DistrictId
                           ORDER BY
                               CASE WHEN ds.Role = 'Primary' THEN 0 ELSE 1 END,
                               s.Name;
                           """;

        await using var connection = new SqlConnection(connectionString);

        var rows = await connection.QueryAsync(
            sql,
            new { DistrictId = districtId });

        return
        [
            .. rows
                .Select(row => new Salesperson(
                    row.Id,
                    row.Name,
                    Enum.Parse<SalespersonRole>(row.Role)))
        ];
    }
    
    public async Task<List<Store>> GetStoresAsync(int districtId)
    {
        const string sql = """
                           SELECT Id, Name
                           FROM dbo.Store
                           WHERE DistrictId = @DistrictId
                           ORDER BY Name;
                           """;

        await using var connection = new SqlConnection(connectionString);

        var stores = await connection.QueryAsync<Store>(
            sql,
            new { DistrictId = districtId });

        return [.. stores];
    }
}