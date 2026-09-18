namespace Districts.Api.ModelDtos;

public class DistrictDto(int id, string name)
{
    public int Id { get; } = id;
    public string Name { get; } = name;
}