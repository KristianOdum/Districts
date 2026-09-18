using Districts.Domain.Models;

namespace Districts.Api.Dtos;

public class SalespersonDto(int id, string name, string role)
{
    public int Id { get; } = id;
    public string Name { get; } = name;
    public string Role { get; } = role;
    
    public static SalespersonDto FromDomain(Salesperson salesperson)
    {
        return new SalespersonDto(
            id: salesperson.Id,
            name: salesperson.Name,
            role: salesperson.Role.ToString()
        );
    }
}