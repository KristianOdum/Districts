using System.ComponentModel.DataAnnotations;
using Districts.Domain.Models;

namespace Districts.Api.Dtos;

public record RemoveSalespersonFromDistrictDto(
    int SalespersonId,
    SalespersonRole Role);