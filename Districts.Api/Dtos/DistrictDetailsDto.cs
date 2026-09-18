using Districts.Application.Districts.Queries;

namespace Districts.Api.Dtos;

public class DistrictDetailsDto(
    int id,
    string name,
    List<SalespersonDto> salespersons,
    List<StoreDto> stores)
{
    public int Id { get; } = id;
    public string Name { get; } = name;
    public List<SalespersonDto> Salespersons { get; } = salespersons;
    public List<StoreDto> Stores { get; } = stores;

    public static DistrictDetailsDto FromApplication(DistrictDetails details)
    {
        return new DistrictDetailsDto(
            id: details.Id,
            name: details.Name,
            salespersons: [.. details.Salespersons.Select(SalespersonDto.FromDomain)],
            stores: [.. details.Stores.Select(StoreDto.FromDomain)]
        );
    }
}