using System.Net.Http;
using System.Net.Http.Json;
using Districts.Wpf.Models;
using Microsoft.Extensions.Logging;

namespace Districts.Wpf.Services;

public class DistrictApiClient(HttpClient httpClient, ILogger<DistrictApiClient> logger)
{
    public async Task<List<DistrictModel>> GetDistrictsAsync()
    {
        var districts = await httpClient.GetFromJsonAsync<List<DistrictModel>>(
            "api/districts");

        return districts ?? [];
    }

    public async Task<DistrictDetailsModel?> GetDistrictDetailsAsync(int districtId)
    {
        var district = await httpClient.GetFromJsonAsync<DistrictDetailsModel>($"api/Districts/{districtId}");

        if (district is null)
        {
            logger.LogWarning("District with id {DistrictId} not found", districtId);
        }
        else
        {
            logger.LogInformation("Fetched district with id {DistrictId}", districtId);
        }
        
        return district;
    }
}