using EduPlatform.WPF.ViewModels.GeneralViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EduPlatform.WPF.HostBuilders;

public static class ViewModelExtensions
{
    public static IHostBuilder AddViewModels(this IHostBuilder? hostBuilder)
    {
        ArgumentNullException.ThrowIfNull(hostBuilder, nameof(hostBuilder));

        hostBuilder.ConfigureServices(
            (context, services) =>
            {
                services.AddSingleton<HubViewModel>();
                services.AddSingleton<MainViewModel>();
            });

        return hostBuilder;
    }
}