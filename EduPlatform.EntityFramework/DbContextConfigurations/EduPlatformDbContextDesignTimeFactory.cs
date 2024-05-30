using System.Data;
using EduPlatform.EntityFramework.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace EduPlatform.EntityFramework.DbContextConfigurations
{
    // ToDo: Rename project EduPlatformApp -> EduPlatform.WPF
    public class EduPlatformDbContextDesignTimeFactory : IDesignTimeDbContextFactory<EduPlatformDbContext>
    {
        public EduPlatformDbContext CreateDbContext(string[] args)
        {
            string connectionString = ConfigurationFactory
                                          .GetConfiguration(jsonFileName: "appsettings.json")
                                          .GetConnectionString(name: "MSSQL Server")
                                      ?? throw new DataException("Cannot get connection string from json file");
            
            DbContextOptions options = new DbContextOptionsBuilder()
                .UseSqlServer(connectionString).Options;
            return new EduPlatformDbContext(options);
        }
    }
}
