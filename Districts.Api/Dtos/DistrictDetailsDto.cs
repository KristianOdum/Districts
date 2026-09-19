using Districts.Application.Districts.Queries;

namespace Districts.Api.Dtos;

public class DistrictDetailsDto(
    int id,
    string name,
    SalespersonDto primarySalesperson,
    List<SalespersonDto> secondarySalespersons,
    List<StoreDto> stores)
{
    public int Id { get; } = id;
    public string Name { get; } = name;
    public SalespersonDto PrimarySalesperson { get; } = primarySalesperson;
    public List<SalespersonDto> SecondarySalespersons { get; } = secondarySalespersons;
    public List<StoreDto> Stores { get; } = stores;

    public static DistrictDetailsDto FromApplication(DistrictDetails details)
    {
        return new DistrictDetailsDto(
            id: details.Id,
            name: details.Name,
            primarySalesperson: SalespersonDto.FromDomain(details.PrimarySalesperson),
            secondarySalespersons: [.. details.Salespersons.Select(SalespersonDto.FromDomain)],
            stores: [.. details.Stores.Select(StoreDto.FromDomain)]
        );
    }
}