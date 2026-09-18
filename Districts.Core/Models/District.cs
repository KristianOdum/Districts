namespace Districts.Core.Models;

public class District(int id, string name)
{
    public int Id { get; } = id;
    public string Name { get; } = name;
}