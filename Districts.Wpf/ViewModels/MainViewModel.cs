using System.Collections.ObjectModel;
using System.ComponentModel;
using Districts.Wpf.Models;
using Districts.Wpf.Services;
using Districts.Domain.Models;
using Microsoft.Extensions.Logging;

namespace Districts.Wpf.ViewModels;

public class MainViewModel(DistrictApiClient districtApiClient, SalespersonApiClient salespersonApiClient, ILogger<MainViewModel> logger) : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
    
    public ObservableCollection<DistrictModel> Districts { get; } = [];
    
    public Collection<SalespersonModel> Salespersons { get; } = [];  // No need to be Observable (yet)
    
    public async Task LoadDistrictsAsync()
    {
        var districts = await districtApiClient.GetDistrictsAsync();
        
        Districts.Clear();
        foreach (var district in districts)
        {
            Districts.Add(district);
        }
        
        // Looks weird with nothing selected initially, so choose the first one if present
        if (Districts.Count > 0)
        {
            SelectedDistrict = Districts[0];
        }
    }

    public DistrictModel? SelectedDistrict
    {
        get;
        set
        {
            field = value;

            logger.LogInformation(
                "Selected district changed to {DistrictId}",
                value?.Id);
            
            if (value is not null)
            {
                _ = LoadDistrictDetailsAsync(value.Id);
            }
        }
    }

    public DistrictDetailsModel? SelectedDistrictDetails
    {
        get; 
        private set
        {
            field = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedDistrictDetails)));
        }
    }
    
    private async Task LoadDistrictDetailsAsync(int districtId)
    {
        SelectedDistrictDetails =
            await districtApiClient.GetDistrictDetailsAsync(districtId);
    }
    
    public async Task LoadSalespersonsAsync()
    {
        var salespersons = await salespersonApiClient.GetSalespersonsAsync();
        
        Salespersons.Clear();
        foreach (var salesperson in salespersons)
        {
            Salespersons.Add(salesperson);
        }
    }
    
    public async Task AddSalespersonAsync(
        int salespersonId,
        SalespersonRole role)
    {
        if (SelectedDistrict is null)
        {
            return;
        }

        SelectedDistrictDetails =
            await districtApiClient.AddSalespersonToDistrictAsync(
                SelectedDistrict.Id,
                salespersonId,
                role);
    }

    public async Task RemoveSalespersonAsync(
        int salespersonId,
        SalespersonRole role)
    {
        if (SelectedDistrict is null)
        {
            return;
        }

        SelectedDistrictDetails =
            await districtApiClient.RemoveSalespersonFromDistrictAsync(
                SelectedDistrict.Id,
                salespersonId,
                role);
    }
}