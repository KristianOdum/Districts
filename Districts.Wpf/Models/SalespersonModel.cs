namespace Districts.Wpf.Models;

public class SalespersonModel(
    int id,
    string name,
    string role)
{
    public int Id { get; } = id;
    public string Name { get; } = name;
    public string Role { get; } = role;
}