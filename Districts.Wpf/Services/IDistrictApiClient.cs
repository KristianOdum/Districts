using System.Net.Http;
using System.Net.Http.Json;
using Districts.Domain.Models;
using Districts.Wpf.Models;
using Microsoft.Extensions.Logging;

namespace Districts.Wpf.Services;

public interface IDistrictApiClient
{
    public Task<List<DistrictModel>> GetDistrictsAsync();

    public Task<List<SalespersonModel>> GetSalespersonsAsync();

    public Task<DistrictDetailsModel?> GetDistrictDetailsAsync(int districtId);

    public Task<DistrictDetailsModel?> AddSalespersonToDistrictAsync(int districtId, int salespersonId,
        SalespersonRole salespersonRole);

    public Task<DistrictDetailsModel?> RemoveSalespersonFromDistrictAsync(
        int districtId,
        int salespersonId,
        SalespersonRole role);
}