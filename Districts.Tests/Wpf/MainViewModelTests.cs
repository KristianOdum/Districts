using Districts.Wpf.Models;
using Districts.Wpf.Services;
using Districts.Wpf.ViewModels;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace Districts.Tests.Wpf;

[TestFixture]
public class MainViewModelTests
{
    private Mock<IDistrictApiClient> _districtApiClient = null!;
    private Mock<ISalespersonApiClient> _salespersonApiClient = null!;
    private MainViewModel _viewModel = null!;

    [SetUp]
    public void SetUp()
    {
        _districtApiClient = new Mock<IDistrictApiClient>();
        _salespersonApiClient = new Mock<ISalespersonApiClient>();

        var logger = Mock.Of<ILogger<MainViewModel>>();

        _viewModel = new MainViewModel(
            _districtApiClient.Object,
            _salespersonApiClient.Object,
            logger);
    }

    [Test]
    public async Task LoadDistrictsAsync_LoadsDistrictsAndSelectsFirstDistrict()
    {
        // Arrange
        var districts = new List<DistrictModel>
        {
            new(
                1,
                "Northern Denmark",
                new SalespersonModel(1, "Julie Jensen")),

            new(
                2,
                "Southern Denmark",
                new SalespersonModel(2, "Frederik Hansen"))
        };

        _districtApiClient
            .Setup(client => client.GetDistrictsAsync())
            .ReturnsAsync(districts);

        // Act
        await _viewModel.LoadDistrictsAsync();

        using (Assert.EnterMultipleScope())
        {
            // Assert
            Assert.That(_viewModel.Districts, Has.Count.EqualTo(2));
            Assert.That(
                _viewModel.SelectedDistrict?.Id,
                Is.EqualTo(1));
        }
    }

    [Test]
    public async Task LoadSalespersonsAsync_LoadsSalespersons()
    {
        // Arrange
        var salespersons = new List<SalespersonModel>
        {
            new(1, "Julie Jensen"),
            new(2, "Frederik Hansen")
        };

        _salespersonApiClient
            .Setup(client => client.GetSalespersonsAsync())
            .ReturnsAsync(salespersons);

        // Act
        await _viewModel.LoadSalespersonsAsync();

        // Assert
        Assert.That(_viewModel.Salespersons, Has.Count.EqualTo(2));
        Assert.That(
            _viewModel.Salespersons[0].Name,
            Is.EqualTo("Julie Jensen"));
    }

    [Test]
    public async Task SelectingDistrict_LoadsDistrictDetails()
    {
        // Arrange
        var district = new DistrictModel(
            1,
            "Northern Denmark",
            new SalespersonModel(1, "Julie Jensen"));

        var districtDetails = new DistrictDetailsModel(
            1,
            "Northern Denmark",
            new SalespersonModel(1, "Julie Jensen"),
            [
                new SalespersonModel(2, "Søren Andreasen")
            ],
            [
                new StoreModel(1, "Alimentum"),
                new StoreModel(2, "Bach & Nurup")
            ]);

        _districtApiClient
            .Setup(client => client.GetDistrictDetailsAsync(1))
            .ReturnsAsync(districtDetails);

        // Act
        _viewModel.SelectedDistrict = district;

        // Wait for the async operation started by the setter.
        var timeout = TimeSpan.FromSeconds(1);
        var start = DateTime.UtcNow;

        while (_viewModel.SelectedDistrictDetails is null &&
               DateTime.UtcNow - start < timeout)
            await Task.Yield();

        using (Assert.EnterMultipleScope())
        {
            // Assert
            Assert.That(
                _viewModel.SelectedDistrictDetails.Name,
                Is.EqualTo("Northern Denmark"));

            Assert.That(
                _viewModel.SelectedDistrictDetails.PrimarySalesperson.Name,
                Is.EqualTo("Julie Jensen"));

            Assert.That(
                _viewModel.SelectedDistrictDetails.SecondarySalespersons,
                Has.Count.EqualTo(1));

            Assert.That(
                _viewModel.SelectedDistrictDetails.Stores,
                Has.Count.EqualTo(2));
        }
    }
}