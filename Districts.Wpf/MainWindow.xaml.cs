using System.Windows;
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
    }
}