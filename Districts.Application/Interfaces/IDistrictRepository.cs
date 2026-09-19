using Districts.Application.Districts.Queries;
using Districts.Domain.Models.Base;

namespace Districts.Application.Interfaces;

public interface IDistrictRepository
{
    // Get them all
    Task<List<District>> GetDistrictsAsync();
    
    // Get details
    Task<District?> GetDistrictAsync(int districtId);
    Task<List<Salesperson>> GetSecondarySalespersonsAsync(int districtId);
    Task<List<Store>> GetStoresAsync(int districtId);
}