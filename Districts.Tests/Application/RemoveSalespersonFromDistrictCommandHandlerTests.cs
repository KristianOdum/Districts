using Districts.Application.Commands;
using Districts.Application.Exceptions;
using Districts.Application.Interfaces;
using Districts.Domain.Models;
using NSubstitute;
using NUnit.Framework;

namespace Districts.Tests.Application;

[TestFixture]
public class RemoveSalespersonFromDistrictCommandHandlerTests
{
    [Test]
    public void RemovePrimarySalesperson_ThrowsBusinessRuleViolationException()
    {
        // Arrange
        var repository = Substitute.For<IDistrictRepository>();
        var handler = new RemoveSalespersonFromDistrictCommandHandler(repository);

        var command = new RemoveSalespersonFromDistrict(
            1,
            2,
            SalespersonRole.Primary);

        // Act + Assert
        Assert.ThrowsAsync<BusinessRuleViolationException>(() => handler.HandleAsync(command));
    }
}