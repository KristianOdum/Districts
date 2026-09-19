using Districts.Domain.Models.Base;

namespace Districts.Api.Dtos;

public class SalespersonDto(int id, string name)
{
    public int Id { get; } = id;
    public string Name { get; } = name;
    
    public static SalespersonDto FromDomain(Salesperson salesperson)
    {
        return new SalespersonDto(
            id: salesperson.Id,
            name: salesperson.Name
        );
    }
}