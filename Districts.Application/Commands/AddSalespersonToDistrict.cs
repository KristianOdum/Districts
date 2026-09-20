using Districts.Domain.Models;

namespace Districts.Application.Commands;

public record AddSalespersonToDistrict(
    int DistrictId,
    int SalespersonId,
    SalespersonRole Role);