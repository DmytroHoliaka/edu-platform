using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace EduPlatform.EntityFramework.DbContextConfigurations
{
    public class EduPlatformDbContextDesignTimeFactory : IDesignTimeDbContextFactory<EduPlatformDbContext>
    {
        public EduPlatformDbContext CreateDbContext(string[] args)
        {
            // ToDo: Use JSON configuration file #2
            const string connectionString = @"Server=(localdb)\mssqllocaldb;Database=EduPlatform;Trusted_Connection=True;";
            DbContextOptions options = new DbContextOptionsBuilder()
                .UseSqlServer(connectionString).Options;
            return new EduPlatformDbContext(options);
        }
    }
}
