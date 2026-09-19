using System.Collections.ObjectModel;
using System.ComponentModel;
using Districts.Wpf.Models;
using Districts.Wpf.Services;
using Microsoft.Extensions.Logging;

namespace Districts.Wpf.ViewModels;

public class MainViewModel(DistrictApiClient districtApiClient, ILogger<MainViewModel> logger) : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
    
    public ObservableCollection<DistrictModel> Districts { get; } = [];
    
    public async Task LoadDistrictsAsync()
    {
        var districts = await districtApiClient.GetDistrictsAsync();

        foreach (var district in districts)
        {
            Districts.Add(district);
        }
        
        
        // Looks weird with nothing selected initially
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
    
}