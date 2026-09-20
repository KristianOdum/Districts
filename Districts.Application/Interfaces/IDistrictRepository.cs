using Districts.Application.Queries;
using Districts.Domain.Models;

namespace Districts.Application.Interfaces;

public interface IDistrictRepository
{
    // Queries
    Task<List<District>> GetDistrictsAsync();
    Task<District?> GetDistrictAsync(int districtId);
    Task<List<Salesperson>> GetSecondarySalespersonsAsync(int districtId);
    Task<List<Store>> GetStoresAsync(int districtId);
    
    // Commands
    Task SetPrimarySalespersonAsync(
        int districtId,
        int salespersonId);

    Task AddSecondarySalespersonAsync(
        int districtId,
        int salespersonId);

    Task<bool> RemoveSecondarySalespersonAsync(
        int districtId,
        int salespersonId);
}