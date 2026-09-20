using System.Windows;
using System.Windows.Controls;
using Districts.Domain.Models;
using Districts.Wpf.Models;
using Districts.Wpf.ViewModels;

namespace Districts.Wpf;

public partial class MainWindow : Window
{
    private readonly MainViewModel _viewModel;
    
    public MainWindow(MainViewModel viewModel)
    {
        InitializeComponent();
        
        _viewModel = viewModel;
        DataContext = viewModel;
    }
    
    private async void Window_Loaded(object sender, RoutedEventArgs e)
    {
        await _viewModel.LoadDistrictsAsync();
        await _viewModel.LoadSalespersonsAsync();
        DistrictListBox.SelectedIndex = 0;
    }
    
    private async void EditPrimarySalesperson_Click(
        object sender,
        RoutedEventArgs e)
    {
        if (_viewModel.SelectedDistrictDetails is null)
        {
            return;
        }

        var dialog = new SalespersonDialog(
            salespersons: _viewModel.Salespersons,
            excludedSalespersonIds: [_viewModel.SelectedDistrictDetails.PrimarySalesperson.Id],
            SalespersonRole.Primary)
        {
            Owner = this
        };

        if (dialog.ShowDialog() != true)
        {
            return;
        }

        await _viewModel.AddSalespersonAsync(
            dialog.SelectedSalesperson!.Id,
            SalespersonRole.Primary);
    }
    
    private async void AddSalesperson_Click(
        object sender,
        RoutedEventArgs e)
    {
        if (_viewModel.SelectedDistrictDetails is null)
        {
            return;
        }
        
        var excludedIds = _viewModel.SelectedDistrictDetails
            .SecondarySalespersons
            .Select(s => s.Id);

        var dialog = new SalespersonDialog(
            salespersons: _viewModel.Salespersons, excludedSalespersonIds: excludedIds, SalespersonRole.Secondary)
        {
            Owner = this
        };

        if (dialog.ShowDialog() != true)
        {
            return;
        }

        await _viewModel.AddSalespersonAsync(
            dialog.SelectedSalesperson!.Id,
            SalespersonRole.Secondary);
    }
    
    private async void DeleteSecondarySalesperson_Click(
        object sender,
        RoutedEventArgs e)
    {
        if (sender is not Button button ||
            button.DataContext is not SalespersonModel salesperson)
        {
            return;
        }

        if (_viewModel.SelectedDistrict is null)
        {
            return;
        }

        await _viewModel.RemoveSalespersonAsync(
            salesperson.Id,
            SalespersonRole.Secondary);
    }
}