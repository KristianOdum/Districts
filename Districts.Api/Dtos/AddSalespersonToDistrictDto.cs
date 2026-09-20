using Districts.Domain.Models;

namespace Districts.Api.Dtos;

public record AddSalespersonToDistrictDto(
    int SalespersonId,
    SalespersonRole Role);