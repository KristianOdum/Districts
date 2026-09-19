namespace Districts.Domain.Models.Base;

public class District(int id, string name, Salesperson primarySalesperson)
{
    public int Id { get; } = id;
    public string Name { get; } = name;

    public Salesperson PrimarySalesperson { get; } = primarySalesperson;
}