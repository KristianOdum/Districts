using Districts.Domain.Models;

namespace Districts.Application.Districts.Queries;

public class DistrictDetails(
    int id,
    string name,
    List<Salesperson> salespersons,
    List<Store> stores)
{
    public int Id { get; } = id;
    public string Name { get; } = name;
    public List<Salesperson> Salespersons { get; } = salespersons;
    public List<Store> Stores { get; } = stores;
}