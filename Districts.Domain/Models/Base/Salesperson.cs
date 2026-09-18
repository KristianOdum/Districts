namespace Districts.Domain.Models;

public class Salesperson(int id, string name, SalespersonRole role)
{
    public int Id { get; } = id;
    public string Name { get; } = name;
    public SalespersonRole Role { get; } = role;
}