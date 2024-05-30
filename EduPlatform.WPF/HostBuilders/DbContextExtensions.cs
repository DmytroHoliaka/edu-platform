using System.Data;
using EduPlatform.EntityFramework.DbContextConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EduPlatform.WPF.HostBuilders;

public static class DbContextExtensions
{
    public static IHostBuilder AddDbContext(this IHostBuilder hostBuilder)
    {
        hostBuilder.ConfigureServices(
            (context, services) =>
            {
                string connectionString = context.Configuration.GetConnectionString(name: "MSSQL Server")
                                          ?? throw new DataException("Cannot get connection string from configuration");

                services.AddSingleton<DbContextOptions>(
                    new DbContextOptionsBuilder().UseSqlServer(connectionString).Options);
                services.AddSingleton<EduPlatformDbContextFactory>();
            });

        return hostBuilder;
    }
}