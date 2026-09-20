namespace Districts.Domain.Models;

public class Salesperson(
    int id,
    string employeeNumber,
    string name)
{
    public int Id { get; } = id;
    public string EmployeeNumber { get; } = employeeNumber;
    public string Name { get; } = name;
}