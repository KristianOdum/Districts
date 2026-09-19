namespace Districts.Wpf.Models;

public class DistrictDetailsModel(
    int id,
    string name,
    SalespersonModel primarySalesperson,
    List<SalespersonModel> secondarySalespersons,
    List<StoreModel> stores)
{
    public int Id { get; } = id;
    public string Name { get; } = name;
    public SalespersonModel PrimarySalesperson { get; } = primarySalesperson;
    public List<SalespersonModel> SecondarySalespersons { get; } = secondarySalespersons;
    public List<StoreModel> Stores { get; } = stores;
}