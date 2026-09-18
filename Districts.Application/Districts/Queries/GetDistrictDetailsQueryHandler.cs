using Districts.Application.Interfaces;

namespace Districts.Application.Districts.Queries;

public class GetDistrictDetailsQueryHandler(
    IDistrictRepository districtRepository)
{
    public async Task<DistrictDetails?> HandleAsync(
        GetDistrictDetailsQuery query)
    {
        var district = await districtRepository.GetDistrictAsync(
            query.DistrictId);

        if (district is null)
        {
            return null;
        }

        var salespersons = await districtRepository.GetSalespersonsAsync(
            query.DistrictId);

        var stores = await districtRepository.GetStoresAsync(
            query.DistrictId);

        return new DistrictDetails(
            district.Id,
            district.Name,
            salespersons,
            stores);
    }
}