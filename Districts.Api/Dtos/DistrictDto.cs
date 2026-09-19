using Districts.Domain.Models.Base;

namespace Districts.Api.Dtos;

public class DistrictDto(int id, string name, SalespersonDto salesperson)
{
    public int Id { get; } = id;
    public string Name { get; } = name;
    public SalespersonDto Salesperson { get; } = salesperson;

    public static DistrictDto FromDomain(District district)
    {
        return new DistrictDto(
            id: district.Id,
            name: district.Name,
            salesperson: SalespersonDto.FromDomain(district.PrimarySalesperson)
        );
    }
}