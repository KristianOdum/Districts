using Districts.Domain.Models;

namespace Districts.Api.Dtos;

public class StoreDto(int id, string name)
{
    public int Id { get; } = id;
    public string Name { get; } = name;
    
    public static StoreDto FromDomain(Store salesperson)
    {
        return new StoreDto(
            id: salesperson.Id,
            name: salesperson.Name
        );
    }
}