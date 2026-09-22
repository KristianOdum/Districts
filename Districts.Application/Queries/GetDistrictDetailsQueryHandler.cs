using Districts.Application.Interfaces;

namespace Districts.Application.Queries;

public class GetDistrictDetailsQueryHandler(
    IDistrictRepository districtRepository)
{
    public async Task<DistrictDetails?> HandleAsync(
        GetDistrictDetailsQuery query)
    {
        var district = await districtRepository.GetDistrictAsync(
            query.DistrictId);

        if (district is null) return null;

        var secondarySalespersons = await districtRepository.GetSecondarySalespersonsAsync(
            query.DistrictId);

        var stores = await districtRepository.GetStoresAsync(
            query.DistrictId);

        return new DistrictDetails(
            district.Id,
            district.Name,
            district.PrimarySalesperson,
            secondarySalespersons,
            stores);
    }
}