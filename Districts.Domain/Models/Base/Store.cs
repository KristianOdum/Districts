namespace Districts.Domain.Models;

public class Store(int id, string name)
{
    public int Id { get; } = id;
    public string Name { get; } = name;
}