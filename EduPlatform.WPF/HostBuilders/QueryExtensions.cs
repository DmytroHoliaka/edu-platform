using EduPlatform.Domain.Queries;
using EduPlatform.EntityFramework.Queries;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EduPlatform.WPF.HostBuilders;

public static class QueryExtensions
{
    public static IHostBuilder AddQueries(this IHostBuilder? hostBuilder)
    {
        ArgumentNullException.ThrowIfNull(hostBuilder, nameof(hostBuilder));

        hostBuilder.ConfigureServices(
            (context, services) =>
            {
                services.AddSingleton<IGetAllCoursesQuery, GetAllCoursesQuery>();
                services.AddSingleton<IGetAllGroupsQuery, GetAllGroupsQuery>();
                services.AddSingleton<IGetAllStudentsQuery, GetAllStudentsQuery>();
                services.AddSingleton<IGetAllTeachersQuery, GetAllTeachersQuery>();
            });

        return hostBuilder;
    }
}