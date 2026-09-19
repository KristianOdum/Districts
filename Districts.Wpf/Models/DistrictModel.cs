namespace Districts.Wpf.Models;

public class DistrictModel(int id, string name, SalespersonModel primarySalesperson)
{
    public int Id { get; } = id;
    public string Name { get; } = name;
    public SalespersonModel PrimarySalesperson { get; } = primarySalesperson;
}