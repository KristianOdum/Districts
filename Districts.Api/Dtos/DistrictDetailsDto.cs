using System.ComponentModel.DataAnnotations;
using Districts.Application.Queries;

namespace Districts.Api.Dtos;

public record DistrictDetailsDto(
    int Id,
    string Name,
    SalespersonDto PrimarySalesperson,
    List<SalespersonDto> SecondarySalespersons,
    List<StoreDto> Stores)
{
    public static DistrictDetailsDto FromApplication(DistrictDetails details)
    {
        return new DistrictDetailsDto(
            details.Id,
            details.Name,
            SalespersonDto.FromDomain(details.PrimarySalesperson),
            [.. details.Salespersons.Select(SalespersonDto.FromDomain)],
            [.. details.Stores.Select(StoreDto.FromDomain)]);
    }
}