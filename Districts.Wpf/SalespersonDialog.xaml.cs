using System.Windows;
using System.Windows.Controls;
using Districts.Domain.Models;
using Districts.Wpf.Models;

namespace Districts.Wpf;

public partial class SalespersonDialog : Window
{
    private readonly List<SalespersonModel> _relevantSalespersons;

    public SalespersonModel? SelectedSalesperson { get; private set; }

    public SalespersonDialog(
        IEnumerable<SalespersonModel> salespersons,
        IEnumerable<int>? excludedSalespersonIds,
        SalespersonRole role)
    {
        InitializeComponent();
        
        var excludedIds = excludedSalespersonIds?.ToHashSet() ?? [];
        _relevantSalespersons =
        [
            .. salespersons
                .Where(s => !excludedIds.Contains(s.Id))
        ];

        SalespersonListBox.ItemsSource = _relevantSalespersons;
        
        // if (selectedSalesperson is not null)
        // {
        //     SelectedSalesperson = selectedSalesperson;
        //     SalespersonSearchBox.Text = selectedSalesperson.Name;
        // }
        
        Loaded += (_, _) =>
        {
            SalespersonSearchBox.Focus();
        };
    }

    private void SalespersonSearchBox_TextChanged(
        object sender,
        TextChangedEventArgs e)
    {
        var search = SalespersonSearchBox.Text.Trim();

        SalespersonListBox.ItemsSource =
            _relevantSalespersons
                .Where(s => s.Name.Contains(
                    search,
                    StringComparison.OrdinalIgnoreCase))
                .ToList();
    }

    private void SalespersonListBox_SelectionChanged(
        object sender,
        SelectionChangedEventArgs e)
    {
        if (SalespersonListBox.SelectedItem is not SalespersonModel salesperson)
        {
            return;
        }

        SelectedSalesperson = salesperson;
    }
    
    private void SalespersonListBox_MouseDoubleClick(
        object sender,
        System.Windows.Input.MouseButtonEventArgs e)
    {
        if (SalespersonListBox.SelectedItem is not SalespersonModel)
        {
            return;
        }

        DialogResult = true;
    }

    private void Ok_Click(object sender, RoutedEventArgs e)
    {
        if (SelectedSalesperson is null)
        {
            MessageBox.Show("Please select a salesperson.");
            return;
        }

        DialogResult = true;
    }

    private void Cancel_Click(object sender, RoutedEventArgs e)
    {
        DialogResult = false;
    }
}