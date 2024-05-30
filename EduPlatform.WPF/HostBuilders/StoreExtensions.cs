using EduPlatform.WPF.Stores;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EduPlatform.WPF.HostBuilders;

public static class StoreExtensions
{
    public static IHostBuilder AddStores(this IHostBuilder? hostBuilder)
    {
        ArgumentNullException.ThrowIfNull(hostBuilder, nameof(hostBuilder));
        
        hostBuilder.ConfigureServices(
            (context, services) =>
            {
                services.AddSingleton<ModalNavigationStore>();
                services.AddSingleton<ViewStore>();

                services.AddSingleton<CourseStore>();
                services.AddSingleton<GroupStore>();
                services.AddSingleton<StudentStore>();
                services.AddSingleton<TeacherStore>();
            });

        return hostBuilder;
    }
}