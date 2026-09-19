namespace Districts.Wpf.Models;

public class DistrictDetailsModel(
    int id,
    string name,
    List<SalespersonModel> salespersons,
    List<StoreModel> stores)
{
    public int Id { get; } = id;
    public string Name { get; } = name;
    public List<SalespersonModel> Salespersons { get; } = salespersons;
    public List<StoreModel> Stores { get; } = stores;
}