using System.Windows;
using Districts.Wpf.Services;
using Districts.Wpf.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Districts.Wpf;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private readonly ServiceProvider _serviceProvider;

    public App()
    {
        var services = new ServiceCollection();

        services.AddLogging(logging => logging.AddConsole());

        services.AddHttpClient<IDistrictApiClient, DistrictApiClient>(client =>
        {
            client.BaseAddress = new Uri("http://localhost:5294/");
        });

        services.AddHttpClient<ISalespersonApiClient, SalespersonApiClient>(client =>
        {
            client.BaseAddress = new Uri("http://localhost:5294/");
        });

        services.AddTransient<MainViewModel>();
        services.AddTransient<MainWindow>();

        _serviceProvider = services.BuildServiceProvider();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();

        mainWindow.Show();
    }
}