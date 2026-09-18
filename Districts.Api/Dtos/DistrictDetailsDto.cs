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
}