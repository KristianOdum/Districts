using System.ComponentModel.DataAnnotations;
using Districts.Domain.Models;

namespace Districts.Api.Dtos;

public record SalespersonDto(int Id, string EmployeeNumber, string Name)
{
    public static SalespersonDto FromDomain(Salesperson salesperson) =>
        new(salesperson.Id, salesperson.EmployeeNumber, salesperson.Name);
}