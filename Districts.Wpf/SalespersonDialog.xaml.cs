using System.Windows;
using System.Windows.Controls;
using Districts.Domain.Models;
using Districts.Wpf.Models;

namespace Districts.Wpf;

public partial class SalespersonDialog : Window
{
    private readonly List<SalespersonModel> _salespersons;

    public SalespersonModel? SelectedSalesperson { get; private set; }

    public SalespersonRole SelectedRole =>
        (SalespersonRole)RoleComboBox.SelectedItem;

    public SalespersonDialog(
        IEnumerable<SalespersonModel> salespersons,
        SalespersonModel? selectedSalesperson = null,
        SalespersonRole? fixedRole = null)
    {
        InitializeComponent();

        _salespersons = [.. salespersons];

        SalespersonListBox.ItemsSource = _salespersons;
        
        if (selectedSalesperson is not null)
        {
            SelectedSalesperson = selectedSalesperson;
            SalespersonSearchBox.Text = selectedSalesperson.Name;
        }

        RoleComboBox.ItemsSource =
            Enum.GetValues<SalespersonRole>();

        if (fixedRole.HasValue)
        {
            RoleComboBox.SelectedItem = fixedRole.Value;
            RoleComboBox.IsEnabled = false;
        }
        else
        {
            RoleComboBox.SelectedItem = SalespersonRole.Secondary;
        }
        
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
            _salespersons
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