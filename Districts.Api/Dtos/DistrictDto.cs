namespace Districts.Api.Dtos;

public class DistrictDto(int id, string name)
{
    public int Id { get; } = id;
    public string Name { get; } = name;
}