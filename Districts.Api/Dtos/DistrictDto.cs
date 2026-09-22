using System.ComponentModel.DataAnnotations;
using Districts.Domain.Models;

namespace Districts.Api.Dtos;

public record DistrictDto(
    int Id,
    string Name,
    SalespersonDto PrimarySalesperson)
{
    public static DistrictDto FromDomain(District district)
    {
        return new DistrictDto(
            district.Id,
            district.Name,
            SalespersonDto.FromDomain(district.PrimarySalesperson));
    }
}