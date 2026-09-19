namespace Districts.Wpf.Models;

public class DistrictModel(int id, string name)
{
    public int Id { get; } = id;
    public string Name { get; } = name;
}