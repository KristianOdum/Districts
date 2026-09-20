using Districts.Domain.Models;

namespace Districts.Application.Commands;

public record RemoveSalespersonFromDistrict(
    int DistrictId,
    int SalespersonId,
    SalespersonRole Role);