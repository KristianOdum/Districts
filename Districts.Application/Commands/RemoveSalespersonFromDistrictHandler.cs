using Districts.Application.Exceptions;
using Districts.Application.Interfaces;
using Districts.Domain.Models;

namespace Districts.Application.Commands;

public class RemoveSalespersonFromDistrictCommandHandler(
    IDistrictRepository districtRepository)
{
    public async Task HandleAsync(
        RemoveSalespersonFromDistrict command)
    {
        switch (command.Role)
        {
            case SalespersonRole.Secondary:
                var removed = await districtRepository.RemoveSecondarySalespersonAsync(
                    command.DistrictId,
                    command.SalespersonId);

                if (!removed)
                    throw new KeyNotFoundException(
                        $"Salesperson with ID {command.SalespersonId} " +
                        $"is not assigned as a secondary salesperson " +
                        $"in district with ID {command.DistrictId}.");

                break;

            case SalespersonRole.Primary:
                throw new BusinessRuleViolationException(
                    "A district must always have a primary salesperson.");

            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}