using EduPlatform.WPF.ViewModels.GeneralViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EduPlatform.WPF.HostBuilders;

public static class ViewExtensions
{
    public static IHostBuilder AddViews(this IHostBuilder? hostBuilder)
    {
        ArgumentNullException.ThrowIfNull(hostBuilder, nameof(hostBuilder));

        hostBuilder.ConfigureServices(
            (context, services) =>
            {
                services.AddSingleton<MainWindow>(
                    serviceProvider => new MainWindow()
                    {
                        DataContext = serviceProvider.GetRequiredService<MainViewModel>()
                    });
            });

        return hostBuilder;
    }
}