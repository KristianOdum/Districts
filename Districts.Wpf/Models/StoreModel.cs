namespace Districts.Wpf.Models;

public class StoreModel(
    int id,
    string name)
{
    public int Id { get; } = id;
    public string Name { get; } = name;
}