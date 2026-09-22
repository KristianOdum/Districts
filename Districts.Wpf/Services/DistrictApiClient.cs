using System.Net.Http;
using System.Net.Http.Json;
using Districts.Domain.Models;
using Districts.Wpf.Models;
using Microsoft.Extensions.Logging;

namespace Districts.Wpf.Services;

public class DistrictApiClient(HttpClient httpClient, ILogger<DistrictApiClient> logger) : IDistrictApiClient
{
    public async Task<List<DistrictModel>> GetDistrictsAsync()
    {
        var districts = await httpClient.GetFromJsonAsync<List<DistrictModel>>(
            "api/districts");

        return districts ?? [];
    }

    public async Task<List<SalespersonModel>> GetSalespersonsAsync()
    {
        var salespersons = await httpClient.GetFromJsonAsync<List<SalespersonModel>>(
            "api/salespersons");

        return salespersons ?? [];
    }

    public async Task<DistrictDetailsModel?> GetDistrictDetailsAsync(int districtId)
    {
        var district = await httpClient.GetFromJsonAsync<DistrictDetailsModel>($"api/Districts/{districtId}");

        if (district is null)
            logger.LogWarning("District with id {DistrictId} not found", districtId);
        else
            logger.LogInformation("Fetched district with id {DistrictId}", districtId);

        return district;
    }

    public async Task<DistrictDetailsModel?> AddSalespersonToDistrictAsync(int districtId, int salespersonId,
        SalespersonRole salespersonRole)
    {
        var request = new
        {
            SalespersonId = salespersonId,
            Role = salespersonRole
        };

        var response = await httpClient.PostAsJsonAsync(
            $"api/districts/{districtId}/salespersons", request
        );

        response.EnsureSuccessStatusCode();

        var district = await response.Content
            .ReadFromJsonAsync<DistrictDetailsModel>();

        logger.LogInformation(
            "Added salesperson {SalespersonId} to district {DistrictId} as {Role}",
            salespersonId,
            districtId,
            salespersonRole);

        return district;
    }

    public async Task<DistrictDetailsModel?> RemoveSalespersonFromDistrictAsync(
        int districtId,
        int salespersonId,
        SalespersonRole role)
    {
        var request = new HttpRequestMessage(
            HttpMethod.Delete,
            $"api/districts/{districtId}/salespersons/{salespersonId}?role={role}");

        var response = await httpClient.SendAsync(request);

        response.EnsureSuccessStatusCode();

        var district = await response.Content
            .ReadFromJsonAsync<DistrictDetailsModel>();

        logger.LogInformation(
            "Removed salesperson {SalespersonId} from district {DistrictId} as {Role}",
            salespersonId,
            districtId,
            role);

        return district;
    }
}