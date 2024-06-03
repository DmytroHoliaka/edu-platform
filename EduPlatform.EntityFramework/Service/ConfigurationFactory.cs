using Microsoft.Extensions.Configuration;

namespace EduPlatform.EntityFramework.Service;

public static class ConfigurationFactory
{
    public static IConfiguration GetConfiguration(string? jsonFileName, string? subdirectoryPath = "")
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(jsonFileName, nameof(jsonFileName));
        ArgumentNullException.ThrowIfNull(subdirectoryPath, nameof(subdirectoryPath));

        IConfiguration config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory() + "/" + subdirectoryPath)
            .AddJsonFile(jsonFileName)
            .Build();

        return config;
    }
}