using Districts.Domain.Models;

namespace Districts.Application.Queries;

public class DistrictDetails(
    int id,
    string name,
    Salesperson primarySalesperson,
    List<Salesperson> salespersons,
    List<Store> stores)
{
    public int Id { get; } = id;
    public string Name { get; } = name;
    public Salesperson PrimarySalesperson { get; } = primarySalesperson;
    public List<Salesperson> Salespersons { get; } = salespersons;
    public List<Store> Stores { get; } = stores;
}