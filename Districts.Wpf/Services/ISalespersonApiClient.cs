using System.Net.Http;
using System.Net.Http.Json;
using Districts.Domain.Models;
using Districts.Wpf.Models;
using Microsoft.Extensions.Logging;

namespace Districts.Wpf.Services;

public interface ISalespersonApiClient
{
    public Task<List<SalespersonModel>> GetSalespersonsAsync();
}