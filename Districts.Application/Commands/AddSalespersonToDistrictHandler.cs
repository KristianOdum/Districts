using Districts.Application.Interfaces;
using Districts.Domain.Models;

namespace Districts.Application.Commands;

public class AddSalespersonToDistrictCommandHandler(
    IDistrictRepository districtRepository)
{
    public async Task HandleAsync(
        AddSalespersonToDistrict command)
    {
        switch (command.Role)
        {
            case SalespersonRole.Primary:
                await districtRepository.SetPrimarySalespersonAsync(
                    command.DistrictId,
                    command.SalespersonId);
                break;

            case SalespersonRole.Secondary:
                await districtRepository.AddSecondarySalespersonAsync(
                    command.DistrictId,
                    command.SalespersonId);
                break;

            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}