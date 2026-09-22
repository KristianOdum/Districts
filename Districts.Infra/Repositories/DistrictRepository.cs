using Districts.Domain.Models;
using Microsoft.Data.SqlClient;
using Dapper;
using Districts.Application.Interfaces;

namespace Districts.Infra.Repositories;

public class DistrictRepository(string connectionString) : IDistrictRepository
{
    public async Task<List<District>> GetDistrictsAsync()
    {
        const string sql = """
                           SELECT
                               d.Id,
                               d.Name,
                               s.Id,
                               s.EmployeeNumber,
                               s.Name
                           FROM dbo.District d
                           INNER JOIN dbo.Salesperson s
                               ON s.Id = d.PrimarySalespersonId;
                           """;

        await using var connection = new SqlConnection(connectionString);
        await connection.OpenAsync();

        await using var command = new SqlCommand(sql, connection);
        await using var reader = await command.ExecuteReaderAsync();

        var districts = new List<District>();

        while (await reader.ReadAsync())
        {
            var salesperson = new Salesperson(
                reader.GetInt32(2),
                reader.GetString(3),
                reader.GetString(4));

            var district = new District(
                reader.GetInt32(0),
                reader.GetString(1),
                salesperson);

            districts.Add(district);
        }

        return districts;
    }

    public async Task<District?> GetDistrictAsync(int districtId)
    {
        const string sql = """
                           SELECT
                               d.Id,
                               d.Name,
                               s.Id,
                               s.EmployeeNumber,
                               s.Name
                           FROM dbo.District d
                           INNER JOIN dbo.Salesperson s
                               ON s.Id = d.PrimarySalespersonId
                           WHERE d.Id = @DistrictId
                           """;

        await using var connection = new SqlConnection(connectionString);

        await connection.OpenAsync();

        await using var command = new SqlCommand(sql, connection);
        command.Parameters.AddWithValue("@DistrictId", districtId);

        await using var reader = await command.ExecuteReaderAsync();

        if (!await reader.ReadAsync()) return null;

        var salesperson = new Salesperson(
            reader.GetInt32(2),
            reader.GetString(3),
            reader.GetString(4));

        return new District(
            reader.GetInt32(0),
            reader.GetString(1),
            salesperson);
    }

    public async Task<List<Salesperson>> GetSecondarySalespersonsAsync(int districtId)
    {
        const string sql = """
                           SELECT
                               s.Id,
                               s.EmployeeNumber,
                               s.Name
                           FROM dbo.DistrictSecondarySalesperson ds
                           INNER JOIN dbo.Salesperson s
                               ON s.Id = ds.SalespersonId
                           WHERE ds.DistrictId = @DistrictId
                           ORDER BY
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
                    row.EmployeeNumber,
                    row.Name))
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

    public async Task SetPrimarySalespersonAsync(
        int districtId,
        int salespersonId)
    {
        const string sql = """
                           UPDATE dbo.District
                           SET PrimarySalespersonId = @SalespersonId
                           WHERE Id = @DistrictId;
                           """;

        await using var connection = new SqlConnection(connectionString);

        await connection.ExecuteAsync(
            sql,
            new
            {
                DistrictId = districtId,
                SalespersonId = salespersonId
            });
    }

    public async Task AddSecondarySalespersonAsync(
        int districtId,
        int salespersonId)
    {
        const string sql = """
                           INSERT INTO dbo.DistrictSecondarySalesperson
                               (DistrictId, SalespersonId)
                           VALUES
                               (@DistrictId, @SalespersonId);
                           """;

        await using var connection = new SqlConnection(connectionString);

        await connection.ExecuteAsync(
            sql,
            new
            {
                DistrictId = districtId,
                SalespersonId = salespersonId
            });
    }

    public async Task<bool> RemoveSecondarySalespersonAsync(
        int districtId,
        int salespersonId)
    {
        const string sql = """
                           DELETE FROM dbo.DistrictSecondarySalesperson
                           WHERE DistrictId = @DistrictId
                             AND SalespersonId = @SalespersonId;
                           """;

        await using var connection = new SqlConnection(connectionString);

        var affectedRows = await connection.ExecuteAsync(
            sql,
            new
            {
                DistrictId = districtId,
                SalespersonId = salespersonId
            });

        return affectedRows > 0;
    }
}