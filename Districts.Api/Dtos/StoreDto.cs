using System.ComponentModel.DataAnnotations;
using Districts.Domain.Models;

namespace Districts.Api.Dtos;

public record StoreDto(int Id, string Name)
{
    public static StoreDto FromDomain(Store store)
    {
        return new StoreDto(store.Id, store.Name);
    }
}