using System.Net.Http;
using System.Net.Http.Json;
using Districts.Domain.Models;
using Districts.Wpf.Models;
using Microsoft.Extensions.Logging;

namespace Districts.Wpf.Services;

public class SalespersonApiClient(HttpClient httpClient, ILogger<SalespersonApiClient> logger) : ISalespersonApiClient
{
    public async Task<List<SalespersonModel>> GetSalespersonsAsync()
    {
        var salespersons = await httpClient.GetFromJsonAsync<List<SalespersonModel>>(
            "api/salespersons");

        return salespersons ?? [];
    }
}